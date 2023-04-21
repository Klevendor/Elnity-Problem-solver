using AutoMapper;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using ElnityServerBLL.Tools.JwtUtilities;
using ElnityServerDAL.Constant;
using ElnityServerDAL.Context;
using ElnityServerDAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace ElnityServerBLL.Services.Implementation
{
    public class AccountService : IAccountService
    {
        public ApplicationDbContext _aplicationDbContext { get; }
        
        public RoleManager<IdentityRole<Guid>> _roleManager { get; }
        
        public UserManager<ApplicationUser> _userManager { get; }

        private readonly IMapper _mapper;

        private readonly IJwtUtilities _jwtUtilities;

        public AccountService(ApplicationDbContext aplicationDbContext, 
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager, 
                              RoleManager<IdentityRole<Guid>> roleManager,
                              IJwtUtilities jwtUtilities,
                              IMapper mapper)
        {
            _aplicationDbContext = aplicationDbContext;
            _userManager = userManager;
            _jwtUtilities = jwtUtilities;
            _mapper = mapper;
        }

        public async Task<UserLoginResModel> LoginAsync(UserLoginReqModel user)
        {
            var authenticationModel = new UserLoginResModel();
            var resFindUserAfterLogin = await _userManager.FindByEmailAsync(user.Email);
            if (resFindUserAfterLogin != null)
            {
                if (!await _userManager.IsLockedOutAsync(resFindUserAfterLogin))
                {
                    if (await _userManager.CheckPasswordAsync(resFindUserAfterLogin, user.Password))
                    {
                        authenticationModel.InfoMessages = "Ok";
                        authenticationModel.IsAuthenticated = true;
                        authenticationModel.UserId = resFindUserAfterLogin.Id;
                        // JwtSecurityToken jwtSecurityToken = await CreateJwtToken(resFindUserAfterLogin);
                        authenticationModel.Token = await _jwtUtilities.GenerateJwtToken(resFindUserAfterLogin); //new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                        authenticationModel.Email = resFindUserAfterLogin.Email;
                        authenticationModel.UserName = resFindUserAfterLogin.UserName;
                        var resUserRoles = await _userManager.GetRolesAsync(resFindUserAfterLogin).ConfigureAwait(false);
                        authenticationModel.UserRoles = resUserRoles.ToList();

                        if (resFindUserAfterLogin.RefreshTokens.Any(a => a.IsActive))
                        {
                            var activeRefreshToken = resFindUserAfterLogin.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                            authenticationModel.RefreshToken = activeRefreshToken.Token;
                            authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
                        }
                        else
                        {
                            var refreshToken = _jwtUtilities.GenerateRefreshToken("sd");
                            authenticationModel.RefreshToken = refreshToken.Token;
                            authenticationModel.RefreshTokenExpiration = refreshToken.Expires;
                            resFindUserAfterLogin.RefreshTokens.Add(refreshToken);
                            _aplicationDbContext.Update(resFindUserAfterLogin);
                            await _aplicationDbContext.SaveChangesAsync();
                        }

                        return authenticationModel;
                    }
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.InfoMessages = "Invalid email or password";
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.InfoMessages = "Account was banned";
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.InfoMessages = "Invalid email or password";
            return authenticationModel;
        }

        public async Task<UserLoginResModel> RegisterAsync(UserRegisterReqModel user)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            var resFindUser = await _userManager.FindByNameAsync(user.UserName);
            if(resFindUser == null)
            {
                var result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    var resAddRole = await _userManager.AddToRoleAsync(appUser, AuthorizationSettings.Roles.User.ToString());
                    if (resAddRole.Succeeded)
                    {
                        var resFindUserAfterRegister = await _userManager.FindByNameAsync(user.UserName);
                        var resUserRoles = await _userManager.GetRolesAsync(resFindUserAfterRegister);

                        if (resFindUserAfterRegister != null)
                        {
                            var userResponse = _mapper.Map<UserLoginResModel>(resFindUserAfterRegister);
                            userResponse.InfoMessages = "Ok";
                            userResponse.UserRoles = resUserRoles;
                            return userResponse;
                        }
                        return new UserLoginResModel { InfoMessages = "User not found" };
                    }
                    return new UserLoginResModel { InfoMessages = "No user role added" };
                }
                return new UserLoginResModel { InfoMessages = "Create user error" };
            }
            return new UserLoginResModel { InfoMessages = "UserName already exist" };
        }


        public async Task<ApplicationUser> GetByUserId(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginResModel> RefreshTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public bool RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
