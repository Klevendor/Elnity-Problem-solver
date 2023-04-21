using ElnityServer.Authorization.CustomAttributes;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElnityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet("HelloWorld")]
        public string Hello()
        {
            return "Hello world!";
        }

        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserLoginResModel>> RegisterAsync([FromBody] UserRegisterReqModel userRegister)
        {
            try
            {
                var res = await _accountService.RegisterAsync(userRegister);
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
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserLoginResModel>> LoginAsync([FromBody] UserLoginReqModel userLogin)
        {
            try
            {
                var res = await _accountService.LoginAsync(userLogin);
                if (res.InfoMessages == "Ok")
                {
                   // SetRefreshTokenInCookie(res.RefreshToken);
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

    }
}
