using ElnityServerBLL.Dto.NoteAggregator.Request;
using ElnityServerBLL.Dto.NoteAggregator.Response;
using ElnityServerBLL.Services.Interfaces;
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
    public class NoteAggregator: INoteAggregator
    {
        public ApplicationDbContext _aplicationDbContext { get; }

        public UserManager<ApplicationUser> _userManager { get; }

        private readonly AppEnvironment _appEnvironment;

        public NoteAggregator(ApplicationDbContext aplicationDbContext,
                              UserManager<ApplicationUser> userManager,
                              IOptions<AppEnvironment> appEnvironment)
        {
            _aplicationDbContext = aplicationDbContext;
            _userManager = userManager;
            _appEnvironment = appEnvironment.Value;
        }


        public async Task<bool> AddNoteAsync(AddNoteRequest reqParams)
        {
            var user = await _userManager.FindByEmailAsync(reqParams.Email);

            if (user == null || reqParams.Image==null) { return false; }

            var imagePath = await UploadImage(reqParams.Image, user.BaseRoot);

            _aplicationDbContext.NoteAppUserFields.Add(new NoteApp()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = reqParams.Name,
                Status = reqParams.Status,
                CurrentState = reqParams.CurrentState ?? "",
                Note = reqParams.Note ?? "",
                ImagePath = imagePath,
            });

            await _aplicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoteAsync(DeleteNoteRequest reqParams)
        {
            var note = await _aplicationDbContext.NoteAppUserFields.Where(p => p.Id == reqParams.Id).FirstOrDefaultAsync();

            if (note == null)
                return false;

            //File.Delete(note.ImagePath);

            _aplicationDbContext.Remove(note);

            await _aplicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserNoteResponse>> GetNoteAsync(GetNoteUserRequest reqParams)
        {
            var resList = new List<UserNoteResponse>();
            var user = await _userManager.FindByEmailAsync(reqParams.Email);
            var notes = await _aplicationDbContext.NoteAppUserFields.Where(p=>p.UserId == user.Id).ToListAsync();


            if (user == null || notes == null ) { return resList; }

            foreach (var note in notes)
            {
                resList.Add(new UserNoteResponse()
                {
                    Id = note.Id,
                    Name = note.Name,
                    Status = note.Status,
                    CurrentState = note.CurrentState,
                    Note = note.Note,
                    ImagePath = note.ImagePath,
                });
            }

          
            return resList;
        }

        /* 
         
        Methods for help
         
        */

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
