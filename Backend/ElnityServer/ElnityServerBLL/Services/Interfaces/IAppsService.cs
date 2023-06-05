using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.App.Response;

namespace ElnityServerBLL.Services.Interfaces
{
    public interface IAppsService
    {
        public Task<bool> AddNewAppAsync(AddNewAppRequest newApp);
        public Task<bool> ChangeAppStatus(ChangeAppStatusRequest reqParams);
        public Task<List<AppResponse>> GetAllAppsAsync();
        public Task<AppPreviewResponse> GetAppPreviewAsync(GetAppPreviewRequest reqParams);
        public Task<bool> RegisterAppAsync(RegisterAppRequest reqParams);
        public Task<IEnumerable<UserAppResponse>> GetUserAppsAsync(GetUserAppsRequest reqParams);



    }
}
