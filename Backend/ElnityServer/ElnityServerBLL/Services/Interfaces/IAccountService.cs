using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerDAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ElnityServerBLL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<UserLoginResModel> RegisterAsync(UserRegisterReqModel user);
        public Task<UserLoginResModel> LoginAsync(UserLoginReqModel user);
        public Task<UserLoginResModel> RefreshTokenAsync(string token);
        public Task Logout();
        public Task<ApplicationUser> GetByUserId(Guid id);
        public bool RevokeToken(string token);
        public UserManager<ApplicationUser> _userManager { get; }
    }
}
