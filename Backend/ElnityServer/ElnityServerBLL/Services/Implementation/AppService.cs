using AutoMapper;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.App.Response;
using ElnityServerBLL.Services.Interfaces;
using ElnityServerBLL.Tools.JwtUtilities;
using ElnityServerDAL.Constant;
using ElnityServerDAL.Context;
using ElnityServerDAL.Entities.App;
using ElnityServerDAL.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ElnityServerBLL.Services.Implementation
{
    public class AppService: IAppsService
    {
        public ApplicationDbContext _aplicationDbContext { get; }

        public UserManager<ApplicationUser> _userManager { get; }

        private readonly AppEnvironment _appEnvironment;

        private readonly string BaseWorkPath = "/Store/Default/";


        public AppService(ApplicationDbContext aplicationDbContext,
                              UserManager<ApplicationUser> userManager,
                              IOptions<AppEnvironment> appEnvironment)
        {
            _aplicationDbContext = aplicationDbContext;
            _userManager = userManager;
            _appEnvironment = appEnvironment.Value;
        }

        public async Task<bool> ChangeAppStatus(ChangeAppStatusRequest reqParams)
        {
            var appExist = await _aplicationDbContext.Apps.Where(p => p.Id == reqParams.AppId).FirstOrDefaultAsync();

            if (appExist != null)
                return false;

            appExist.InDevelop = reqParams.NewDevelopingStatus;

            await _aplicationDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<bool> AddNewAppAsync(AddNewAppRequest newApp)
        {
            var appExist = await _aplicationDbContext.Apps.Where(p => p.Name == newApp.Name).FirstOrDefaultAsync();

            if(appExist != null)
                return false;

            var pathApp = GenerateBasicFileStructure(newApp.Name);
            var imagePath = await UploadImage(newApp.Image,pathApp);

            _aplicationDbContext.Apps.Add(new App()
            {
                Id = Guid.NewGuid(),
                Name = newApp.Name,
                Description = newApp.Description,
                ImagePath = imagePath,
                InDevelop = newApp.InDevelop
            });

            await _aplicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AppResponse>> GetAllAppsAsync()
        {
            var resultApps = new List<AppResponse>();
            var apps = await _aplicationDbContext.Apps.OrderBy(p=>p.Name).ToListAsync();


            foreach (var app in apps)
            {
                resultApps.Add(new AppResponse()
                {
                    Id = app.Id,
                    Name = app.Name,
                    Description = app.Description,
                    ImagePath = app.ImagePath,
                    InDevelop = app.InDevelop
                });
            }

            return resultApps;
        }

        public async Task<AppPreviewResponse> GetAppPreviewAsync(GetAppPreviewRequest reqParams)
        {
          
            var user = await _userManager.FindByEmailAsync(reqParams.Email);
            var app = await _aplicationDbContext.Apps.Where(p => p.Id == reqParams.AppId).FirstOrDefaultAsync();
            if (user == null || app == null)
                return new AppPreviewResponse()
                {
                    InDevelop = false,
                    CurrentUserAlredy = false,
                };

            var checkUser = await _aplicationDbContext.JournalUserApps.Where(p=>p.UserId == user.Id && p.AppId == reqParams.AppId).FirstOrDefaultAsync();

            return new AppPreviewResponse()
            {
                Name = app.Name,
                Description = app.Description,
                ImagePath = app.ImagePath,
                InDevelop = app.InDevelop,
                CurrentUserAlredy = (checkUser == null ? false : true),
            };
        }

        public async Task<bool> RegisterAppAsync(RegisterAppRequest reqParams)
        {
            var user = await _userManager.FindByEmailAsync(reqParams.Email);
            var app = await _aplicationDbContext.Apps.Where(p => p.Id == reqParams.AppId).FirstOrDefaultAsync();
            if (user == null || app == null)
                return false;

            _aplicationDbContext.JournalUserApps.Add(new JournalApp()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                AppId = app.Id
            });

            await _aplicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserAppResponse>> GetUserAppsAsync(GetUserAppsRequest reqParams)
        {
            var resultList = new List<UserAppResponse>();
            var user = await _userManager.FindByEmailAsync(reqParams.Email);
            var fields = await _aplicationDbContext.JournalUserApps.Where(p => p.UserId == user.Id).Include(p=>p.App).ToListAsync();
            if (user == null || fields == null)
                return resultList;

            foreach (var field in fields)
            {
                resultList.Add(new UserAppResponse()
                {
                    Id = field.App.Id,
                    Name = field.App.Name,
                    ImagePath = field.App.ImagePath
                });
            }

            return resultList;
        }

        /* 
         
        Methods for help
         
        */

        private string GenerateBasicFileStructure(string appName)
        {
            var generatedFolderName = "App_" + appName.Replace(" ","_");
            var pathRoot = Path.GetFullPath(_appEnvironment.WebRootPath + BaseWorkPath + generatedFolderName);
            Directory.CreateDirectory(pathRoot);

            return BaseWorkPath + generatedFolderName;
        }

        private string GenerateHashedName(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                var fileName = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return fileName;
            }
        }

        private async Task<string> UploadImage(IFormFile image, string rootOfUser)
        {
            if (image != null && image.Length > 0)
            {
                //var pathResources = Path.GetFullPath(_appEnvironment.WebRootPath + Path.Combine(rootOfUser));

                var generatedFileName = GenerateHashedName(image.FileName) + Path.GetExtension(image.FileName);
                var imagePath = Path.GetFullPath(_appEnvironment.WebRootPath + Path.Combine(rootOfUser, generatedFileName));
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                imagePath = rootOfUser + "/" + generatedFileName;
                return imagePath;
            }
            return "";
        }
    }
}
