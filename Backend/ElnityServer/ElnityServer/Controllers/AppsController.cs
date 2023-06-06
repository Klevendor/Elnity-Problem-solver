using ElnityServer.Authorization.CustomAttributes;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.App.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElnityServer.Controllers
{
    [Route("api/app")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        private IAppsService _appsService;

        public AppsController(IAppsService appsService)
        {
            _appsService = appsService;
        }

        //[Authorize]
        [HttpPost("add-new-app")]
        public async Task<ActionResult<bool>> AddNewApp([FromForm] AddNewAppRequest addApp)
        {
            var res = await _appsService.AddNewAppAsync(addApp);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("change-app-status")]
        public async Task<ActionResult<bool>> ChangeAppStatus(ChangeAppStatusRequest reqParams)
        {
            var res = await _appsService.ChangeAppStatus(reqParams);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("get-apps")]
        public async Task<ActionResult<IEnumerable<AppResponse>>> GetAllApps()
        {
            var res = await _appsService.GetAllAppsAsync();
            return Ok(res);
        }

        [Authorize]
        [HttpPost("get-app-preview")]
        public async Task<ActionResult<AppPreviewResponse>> GetAppPreview(GetAppPreviewRequest reqParams)
        {
            var res = await _appsService.GetAppPreviewAsync(reqParams);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("register-app")]
        public async Task<ActionResult<bool>> RegisterApp(RegisterAppRequest reqParams)
        {
            var res = await _appsService.RegisterAppAsync(reqParams);
            return Ok(res);
        }

        [Authorize]
        [HttpPost("get-user-apps")]
        public async Task<ActionResult<IEnumerable<UserAppResponse>>> GetUserApps(GetUserAppsRequest reqParams)
        {
            var res = await _appsService.GetUserAppsAsync(reqParams);
            return Ok(res);
        }
    }
}
