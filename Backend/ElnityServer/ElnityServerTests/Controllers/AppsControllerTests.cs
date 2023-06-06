using ElnityServer.Authorization.CustomAttributes;
using ElnityServer.Controllers;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.App.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace ElnityServerTests.Controllers
{
    public class AppsControllerTests
    {
        private AppsController _controller;
        private Mock<IAppsService> _appsServiceMock;

        public AppsControllerTests()
        {
            _appsServiceMock = new Mock<IAppsService>();
            _controller = new AppsController(_appsServiceMock.Object);
        }

        [Fact]
        public async Task AddNewApp_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var addAppRequest = new AddNewAppRequest();

            // Act
            var result = await _controller.AddNewApp(addAppRequest);

            // Assert
            Assert.IsType<ActionResult<bool>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task ChangeAppStatus_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var changeAppStatusRequest = new ChangeAppStatusRequest();

            // Act
            var result = await _controller.ChangeAppStatus(changeAppStatusRequest);

            // Assert
            Assert.IsType<ActionResult<bool>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAllApps_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetAllApps();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<AppResponse>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetAppPreview_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var getAppPreviewRequest = new GetAppPreviewRequest();

            // Act
            var result = await _controller.GetAppPreview(getAppPreviewRequest);

            // Assert
            Assert.IsType<ActionResult<AppPreviewResponse>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task RegisterApp_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var registerAppRequest = new RegisterAppRequest();

            // Act
            var result = await _controller.RegisterApp(registerAppRequest);

            // Assert
            Assert.IsType<ActionResult<bool>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetUserApps_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var getUserAppsRequest = new GetUserAppsRequest();

            // Act
            var result = await _controller.GetUserApps(getUserAppsRequest);

            // Assert
            Assert.IsType<ActionResult<IEnumerable<UserAppResponse>>>(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}