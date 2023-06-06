using System.Collections.Generic;
using System.Threading.Tasks;
using ElnityServer.Controllers;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.App.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ElnityServerTests
{
    public class AppsControllerTests
    {
        private readonly Mock<IAppsService> _appsServiceMock;
        private readonly AppsController _controller;

        public AppsControllerTests()
        {
            _appsServiceMock = new Mock<IAppsService>();
            _controller = new AppsController(_appsServiceMock.Object);
        }

        [Fact]
        public async Task AddNewApp_ReturnsOkResult()
        {
            // Arrange
            var request = new AddNewAppRequest();
            _appsServiceMock.Setup(s => s.AddNewAppAsync(It.IsAny<AddNewAppRequest>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.AddNewApp(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<bool>>(result.Result);
            Assert.True(okResult.Value);
        }

        [Fact]
        public async Task ChangeAppStatus_ReturnsOkResult()
        {
            // Arrange
            var request = new ChangeAppStatusRequest();
            _appsServiceMock.Setup(s => s.ChangeAppStatus(It.IsAny<ChangeAppStatusRequest>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.ChangeAppStatus(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<bool>>(result.Result);
            Assert.True(okResult.Value);
        }

        [Fact]
        public async Task GetAllApps_ReturnsOkResult()
        {
            // Arrange
            var appsResponse = new List<AppResponse>();
            _appsServiceMock.Setup(s => s.GetAllAppsAsync())
                .ReturnsAsync(appsResponse);

            // Act
            var result = await _controller.GetAllApps();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<IEnumerable<AppResponse>>>(result.Result);
            Assert.Equal(appsResponse, okResult.Value);
        }

        [Fact]
        public async Task GetAppPreview_ReturnsOkResult()
        {
            // Arrange
            var request = new GetAppPreviewRequest();
            var appPreviewResponse = new AppPreviewResponse();
            _appsServiceMock.Setup(s => s.GetAppPreviewAsync(It.IsAny<GetAppPreviewRequest>()))
                .ReturnsAsync(appPreviewResponse);

            // Act
            var result = await _controller.GetAppPreview(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<AppPreviewResponse>>(result.Result);
            Assert.Equal(appPreviewResponse, okResult.Value);
        }

        [Fact]
        public async Task RegisterApp_ReturnsOkResult()
        {
            // Arrange
            var request = new RegisterAppRequest();
            _appsServiceMock.Setup(s => s.RegisterAppAsync(It.IsAny<RegisterAppRequest>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.RegisterApp(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<bool>>(result.Result);
            Assert.True(okResult.Value);
        }

        [Fact]
        public async Task GetUserApps_ReturnsOkResult()
        {
            // Arrange
            var request = new GetUserAppsRequest();
            var userAppsResponse = new List<UserAppResponse>();
            _appsServiceMock.Setup(s => s.GetUserAppsAsync(It.IsAny<GetUserAppsRequest>()))
                .ReturnsAsync(userAppsResponse);

            // Act
            var result = await _controller.GetUserApps(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<ActionResult<IEnumerable<UserAppResponse>>>(result.Result);
            Assert.Equal(userAppsResponse, okResult.Value);
        }
    }
}
