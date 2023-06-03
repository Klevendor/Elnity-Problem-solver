using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerDAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ElnityServerBLL.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthenticationResponse> RegisterAsync(RegisterRequest user);
        public Task<AuthenticationResponse> LoginAsync(LoginRequest user, string ipAddress);
        public Task<RefreshDataResponse> RefreshTokenAsync(string token,string ipAddress);
        public Task<ApplicationUser> GetByUserId(Guid id);
        public Task<ApplicationUser> GetByUserName(string userName);
        public Task RevokeTokenAsync(string token, string ipAddress);
        public UserManager<ApplicationUser> _userManager { get; }
    }
}
