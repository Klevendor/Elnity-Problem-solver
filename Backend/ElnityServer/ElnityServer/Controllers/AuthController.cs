using ElnityServer.Authorization.CustomAttributes;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElnityServer.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService accountService)
        {
            _authService = accountService;
        }

        [Authorize]
        [HttpGet("helloworld")]
        public string Hello()
        {
            return "Hello world!";
        }

        [Authorize("Administrator")]
        [HttpGet("helloworldadmin")]
        public string HelloAdmin()
        {
            return "Hello world!";
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAsync([FromBody] RegisterRequest userRegister)
        {
            try
            {
                var res = await _authService.RegisterAsync(userRegister);
                if (res.InfoMessages == "Ok")
                {
                    return Ok(res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> LoginAsync([FromBody] LoginRequest userLogin)
        {
            try
            {
                var res = await _authService.LoginAsync(userLogin, ipAddress());
                if (res.InfoMessages == "Ok")
                {
                    setTokenCookie(res.RefreshToken);
                    return Ok(res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, res);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authService.RefreshTokenAsync(refreshToken,ipAddress());
            if (string.IsNullOrEmpty(response.RefreshToken))
                return BadRequest(response);

            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Authorize("Administrator,User")]
        [HttpPost("revoke-token")]
        public async Task<ActionResult> RevokeToken([FromBody] RevokeTokenRequest tokenModel)
        {
            var token = tokenModel.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            await _authService.RevokeTokenAsync(token, ipAddress());
            return Ok(new { message = "Token revoked" });
        }

        /* 

        Methods for help

        */

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }


        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
