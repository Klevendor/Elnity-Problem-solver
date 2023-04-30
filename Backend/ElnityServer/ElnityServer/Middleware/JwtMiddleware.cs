using ElnityServerBLL.Tools.JwtUtilities;
using ElnityServerBLL.Services.Interfaces;
using ElnityServerDAL.Constant;
using Microsoft.Extensions.Options;

namespace ElnityServer.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;


        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthService accountService, IJwtUtilities jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtUtils.ValidateJwtToken(token);
                if (userId != Guid.Empty)
                {
                    context.Items["User"] = await accountService.GetByUserId(userId);
                    context.Items["Roles"] = await accountService._userManager.GetRolesAsync(await accountService.GetByUserId(userId)).ConfigureAwait(false);
                }
            }
            await _next(context);
        }
    }
}
