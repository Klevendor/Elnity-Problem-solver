using AutoMapper;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using ElnityServerBLL.Tools.JwtUtilities;
using ElnityServerDAL.Constant;
using ElnityServerDAL.Context;
using ElnityServerDAL.Entities.Identity;
using ElnityServerDAL.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;


namespace ElnityServerBLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        public ApplicationDbContext _aplicationDbContext { get; }
        
        public RoleManager<IdentityRole<Guid>> _roleManager { get; }
        
        public UserManager<ApplicationUser> _userManager { get; }

        private readonly IMapper _mapper;

        private readonly IJwtUtilities _jwtUtilities;

        private readonly AppSettings _appSettings;

        public AuthService(ApplicationDbContext aplicationDbContext, 
                              UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager, 
                              RoleManager<IdentityRole<Guid>> roleManager,
                              IOptions<AppSettings> appSettings,
                              IJwtUtilities jwtUtilities,
                              IMapper mapper)
        {
            _aplicationDbContext = aplicationDbContext;
            _userManager = userManager;
            _jwtUtilities = jwtUtilities;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginRequest user, string ipAddress)
        {
            var authenticationModel = new AuthenticationResponse();
            var resFindUserAfterLogin = await _userManager.FindByEmailAsync(user.Email);
            if (resFindUserAfterLogin != null)
            {
                if (!await _userManager.IsLockedOutAsync(resFindUserAfterLogin))
                {
                    if (await _userManager.CheckPasswordAsync(resFindUserAfterLogin, user.Password))
                    {
                        authenticationModel.InfoMessages = "Ok";
                        authenticationModel.IsAuthenticated = true;
                        authenticationModel.Token = await _jwtUtilities.GenerateJwtTokenAsync(resFindUserAfterLogin);
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
                            var refreshToken = _jwtUtilities.GenerateRefreshToken(ipAddress);
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

        public async Task<AuthenticationResponse> RegisterAsync(RegisterRequest user)
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
                            var userResponse = _mapper.Map<AuthenticationResponse>(resFindUserAfterRegister);
                            userResponse.InfoMessages = "Ok";
                            userResponse.UserRoles = resUserRoles;
                            return userResponse;
                        }
                        return new AuthenticationResponse { InfoMessages = "User not found" };
                    }
                    return new AuthenticationResponse { InfoMessages = "No user role added" };
                }
                return new AuthenticationResponse { InfoMessages = "Create user error" };
            }
            return new AuthenticationResponse { InfoMessages = "UserName already exist" };
        }

        public async Task<RefreshDataResponse> RefreshTokenAsync(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);

            if (user == null)
                return new RefreshDataResponse
                {
                    InfoMessages = "Invalid token"
                };

            var refreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == token);

            if (!refreshToken.IsActive)
                return new RefreshDataResponse
                {
                    InfoMessages = "Invalid token"
                };

            revokeRefreshToken(refreshToken, ipAddress);

            var newRefreshToken = _jwtUtilities.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(user);

            _aplicationDbContext.Update(user);
            await _aplicationDbContext.SaveChangesAsync();

            var jwtToken = await _jwtUtilities.GenerateJwtTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            return new RefreshDataResponse
            {
                InfoMessages = "Ok",
                Token = jwtToken,
                Email = user.Email,
                Roles = roles.ToList(),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.Expires
            };
        }

        public async Task RevokeTokenAsync(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            
            if (user != null)
            {
                var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
                revokeRefreshToken(refreshToken,ipAddress);
                _aplicationDbContext.Update(user);
                await _aplicationDbContext.SaveChangesAsync();
            }
        }

        public async Task<ApplicationUser> GetByUserId(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<ApplicationUser> GetByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        /* 
         
        Methods for help
         
        */

        private ApplicationUser getUserByRefreshToken(string token)
        {
            var user = _aplicationDbContext.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            return user;
        }

        private void revokeRefreshToken(RefreshToken token,string ipAddress)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
        }

        private void removeOldRefreshTokens(ApplicationUser user)
        { 
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
    }
}
