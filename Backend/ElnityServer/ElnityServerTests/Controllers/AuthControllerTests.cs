using System;
using System.Threading.Tasks;
using ElnityServer.Authorization.CustomAttributes;
using ElnityServer.Controllers;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ElnityServerTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public void Hello_ReturnsHelloWorld()
        {
            // Arrange

            // Act
            var result = _authController.Hello();

            // Assert
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public void HelloAdmin_WithAdministratorRole_ReturnsHelloWorld()
        {
            // Arrange

            // Act
            var result = _authController.HelloAdmin();

            // Assert
            Assert.Equal("Hello world!", result);
        }

        [Fact]
        public async Task RegisterAsync_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var userRegister = new RegisterRequest();
            var expectedResponse = new AuthenticationResponse { InfoMessages = "Ok" };
            _authServiceMock.Setup(x => x.RegisterAsync(userRegister)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.RegisterAsync(userRegister);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<AuthenticationResponse>(okResult.Value);
            Assert.Equal(expectedResponse, response);
        }

        [Fact]
        public async Task RegisterAsync_InvalidRequest_ReturnsUnprocessableEntityResult()
        {
            // Arrange
            var userRegister = new RegisterRequest();
            var expectedResponse = new AuthenticationResponse { InfoMessages = "Invalid request" };
            _authServiceMock.Setup(x => x.RegisterAsync(userRegister)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.RegisterAsync(userRegister);

            // Assert
            var unprocessableEntityResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status422UnprocessableEntity, unprocessableEntityResult.StatusCode);
            Assert.Equal(expectedResponse, unprocessableEntityResult.Value);

        }

        //[Fact]
        //public async Task LoginAsync_ValidRequest_ReturnsOkResult()
        //{
        //    // Arrange
        //    var userLogin = new LoginRequest();
        //    var expectedResponse = new AuthenticationResponse { InfoMessages = "Ok", RefreshToken = "refreshToken" };
        //    _authServiceMock.Setup(x => x.LoginAsync(userLogin, It.IsAny<string>())).ReturnsAsync(expectedResponse);

        //    // Act
        //    var result = await _authController.LoginAsync(userLogin);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var response = Assert.IsType<AuthenticationResponse>(okResult.Value);
        //    Assert.Equal(expectedResponse, response);
        //    var refreshTokenCookie = _authController.Request.Cookies["refreshToken"];
        //    Assert.Equal("refreshToken", refreshTokenCookie);

        //}

        //[Fact]
        //public async Task LoginAsync_InvalidRequest_ReturnsUnprocessableEntityResult()
        //{
        //    // Arrange
        //    var userLogin = new LoginRequest();
        //    var expectedResponse = new AuthenticationResponse { InfoMessages = "Invalid request" };
        //    _authServiceMock.Setup(x => x.LoginAsync(userLogin, It.IsAny<string>())).ReturnsAsync(expectedResponse);

        //    // Act
        //    var result = await _authController.LoginAsync(userLogin);

        //    // Assert
        //    var unprocessableEntityResult = Assert.IsType<StatusCodeResult>(result.Result);
        //    Assert.Equal(StatusCodes.Status422UnprocessableEntity, unprocessableEntityResult.StatusCode);
        //}


        //[Fact]
        //public async Task RefreshToken_ValidRequest_ReturnsOkResult()
        //{
        //    // Arrange
        //    var refreshToken = "refreshToken";
        //    var ipAddress = "127.0.0.1";
        //    var expectedResponse = new RefreshDataResponse { RefreshToken = "newRefreshToken" };
        //    _authServiceMock.Setup(x => x.RefreshTokenAsync(refreshToken, ipAddress)).ReturnsAsync(expectedResponse);
        //    _authController.Request.Headers["Cookie"] = "refreshToken=" + refreshToken;

        //    // Act
        //    var result = await _authController.RefreshToken();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    var response = Assert.IsType<RefreshDataResponse>(okResult.Value);
        //    Assert.Equal(expectedResponse, response);
        //    _authController.Response.Cookies.Append("refreshToken", "newRefreshToken");
        //}


        //[Fact]
        //public async Task RefreshToken_InvalidRequest_ReturnsBadRequestResult()
        //{
        //    // Arrange
        //    var refreshToken = "refreshToken";
        //    var ipAddress = "127.0.0.1";
        //    var expectedResponse = new RefreshDataResponse();
        //    _authServiceMock.Setup(x => x.RefreshTokenAsync(refreshToken, ipAddress)).ReturnsAsync(expectedResponse);
        //    _authController.Request.Headers["Cookie"] = "refreshToken=" + refreshToken;

        //    // Act
        //    var result = await _authController.RefreshToken();

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        //    Assert.Equal(expectedResponse, badRequestResult.Value);
        //    _authController.Response.Cookies.Delete("refreshToken");
        //}


        //[Fact]
        //public async Task RevokeToken_WithValidToken_ReturnsOkResult()
        //{
        //    // Arrange
        //    var tokenModel = new RevokeTokenRequest { Token = "validToken" };

        //    // Act
        //    var result = await _authController.RevokeToken(tokenModel);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var response = Assert.Equal("Token revoked", okResult.Value);
        //}

        //[Fact]
        //public async Task RevokeToken_WithoutToken_ReturnsBadRequestResult()
        //{
        //    // Arrange
        //    var tokenModel = new RevokeTokenRequest();

        //    // Act
        //    var result = await _authController.RevokeToken(tokenModel);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    var response = Assert.Equal("Token is required", badRequestResult.Value);
        //}

        //[Fact]
        //public void Logout_ClearsTokenCookie_ReturnsOkResult()
        //{
        //    // Arrange

        //    // Act
        //    var result = _authController.Logout();

        //    // Assert
        //    var okResult = Assert.IsType<OkResult>(result);
        //    Assert.Null(_authController.Response.Cookies["refreshToken"]);
        //}

        [Fact]
        public async Task GetUserInfo_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var reqParams = new GetUserInfoRequest { Email = "test@example.com" };
            var expectedResponse = new UserDataResponse();
            _authServiceMock.Setup(x => x.GetUserInfo(reqParams.Email)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.GetUserInfo(reqParams);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<UserDataResponse>(okResult.Value);
            Assert.Equal(expectedResponse, response);
        }

        [Fact]
        public async Task ChangeUserInfo_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var reqParams = new ChangeUserInfoRequest();
            var expectedResponse = true;
            _authServiceMock.Setup(x => x.ChangeUserInfo(reqParams)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.ChangeUserInfo(reqParams);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<bool>(okResult.Value);
            Assert.Equal(expectedResponse, response);
        }
    }
}










































//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ElnityServer.Authorization.CustomAttributes;
//using ElnityServer.Controllers;
//using ElnityServerBLL.Dto.Account.Request;
//using ElnityServerBLL.Dto.Account.Response;
//using ElnityServerBLL.Services.Interfaces;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Xunit;

//namespace ElnityServer.Tests.Controllers
//{
//    public class AuthControllerTests
//    {
//        private readonly AuthController _authController;
//        private readonly Mock<IAuthService> _authServiceMock;

//        public AuthControllerTests()
//        {
//            _authServiceMock = new Mock<IAuthService>();
//            _authController = new AuthController(_authServiceMock.Object);
//        }

//        [Fact]
//        public void Hello_ReturnsHelloWorld()
//        {
//            // Arrange

//            // Act
//            var result = _authController.Hello();

//            // Assert
//            Assert.Equal("Hello world!", result);
//        }

//        [Fact]
//        public void HelloAdmin_WithAdministratorRole_ReturnsHelloWorld()
//        {
//            // Arrange

//            // Act
//            var result = _authController.HelloAdmin();

//            // Assert
//            Assert.Equal("Hello world!", result);
//        }

//        [Fact]
//        public async Task RegisterAsync_ValidRequest_ReturnsOkResult()
//        {
//            // Arrange
//            var userRegister = new RegisterRequest();
//            var expectedResponse = new AuthenticationResponse { InfoMessages = "Ok" };
//            _authServiceMock.Setup(x => x.RegisterAsync(userRegister)).ReturnsAsync(expectedResponse);

//            // Act
//            var result = await _authController.RegisterAsync(userRegister);

//            // Assert
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            var response = Assert.IsType<AuthenticationResponse>(okResult.Value);
//            Assert.Equal(expectedResponse, response);
//        }

//        //[Fact]
//        //public async Task RegisterAsync_InvalidRequest_ReturnsUnprocessableEntityResult()
//        //{
//        //    // Arrange
//        //    var userRegister = new RegisterRequest();
//        //    var expectedResponse = new AuthenticationResponse { InfoMessages = "Invalid request" };
//        //    _authServiceMock.Setup(x => x.RegisterAsync(userRegister)).ReturnsAsync(expectedResponse);

//        //    // Act
//        //    var result = await _authController.RegisterAsync(userRegister);

//        //    // Assert
//        //    var unprocessableEntityResult = Assert.IsType<UnprocessableEntityResult>(result.Result);
//        //    var response = Assert.IsType<AuthenticationResponse>(unprocessableEntityResult.Value);
//        //    Assert.Equal(expectedResponse, response);
//        //}


//    }
//}
