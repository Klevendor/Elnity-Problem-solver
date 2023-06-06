using System;
using System.Linq;
using System.Threading.Tasks;
using ElnityServer.Authorization.CustomAttributes;
using ElnityServer.Controllers;
using ElnityServerBLL.Dto.Account.Request;
using ElnityServerBLL.Dto.Account.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ElnityServerTests
{

    [TestClass]
    public class AuthControllerTests
    {
        [TestMethod]
        public async Task RegisterAsync_ValidUser_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.RegisterAsync(It.IsAny<RegisterRequest>()))
                .ReturnsAsync(new AuthenticationResponse { InfoMessages = "OK" });
            var controller = new AuthController(authServiceMock.Object);
            var userRegister = new RegisterRequest();

            // Act
            var result = await controller.RegisterAsync(userRegister);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Register_ValidUser_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.RegisterAsync(It.IsAny<RegisterRequest>()))
                .ReturnsAsync(new AuthenticationResponse { InfoMessages = "Ok" });
            var controller = new AuthController(authServiceMock.Object);
            var userRegister = new RegisterRequest { /* set necessary properties */ };

            // Act
            var result = await controller.RegisterAsync(userRegister);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<AuthenticationResponse>));
            var actionResult = (ActionResult<AuthenticationResponse>)result;
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            var authenticationResponse = (AuthenticationResponse)okResult.Value;
            Assert.AreEqual("Ok", authenticationResponse.InfoMessages);
        }

        [TestMethod]
        public async Task RegisterAsync_InvalidUser_ReturnsUnprocessableEntity()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.RegisterAsync(It.IsAny<RegisterRequest>()))
                .ReturnsAsync(new AuthenticationResponse { InfoMessages = "Invalid user" });
            var controller = new AuthController(authServiceMock.Object);
            var userRegister = new RegisterRequest();

            // Act
            var result = await controller.RegisterAsync(userRegister);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, objectResult.StatusCode);
        }

        //Unit-тести та тест-кейси для методу LoginAsync:
        
        [TestMethod]
        public async Task LoginAsync_ValidCredentials_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.LoginAsync(It.IsAny<LoginRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new AuthenticationResponse { InfoMessages = "Ok", RefreshToken = "dummyToken" });
            var controller = new AuthController(authServiceMock.Object);
            var userLogin = new LoginRequest();

            // Act
            var result = await controller.LoginAsync(userLogin);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [TestMethod]
        public async Task LoginAsync_InvalidCredentials_ReturnsUnprocessableEntity()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.LoginAsync(It.IsAny<LoginRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new AuthenticationResponse { InfoMessages = "Invalid credentials" });
            var controller = new AuthController(authServiceMock.Object);
            var userLogin = new LoginRequest();

            // Act
            var result = await controller.LoginAsync(userLogin);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;
            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, objectResult.StatusCode);
        }

        //    Unit-тести та тест-кейси для методу RefreshToken:


            [TestMethod]
            public async Task RefreshToken_ValidToken_ReturnsOk()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(service => service.RefreshTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new RefreshDataResponse { RefreshToken = "newToken" });
                var controller = new AuthController(authServiceMock.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
                controller.Request.Cookies.Append("refreshToken", "oldToken");

                // Act
                var result = await controller.RefreshToken();

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                var okResult = (OkObjectResult)result;
                Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            }

            [TestMethod]
            public async Task RefreshToken_InvalidToken_ReturnsBadRequest()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(service => service.RefreshTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(new RefreshDataResponse { RefreshToken = null });
                var controller = new AuthController(authServiceMock.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
                controller.Request.Cookies.Append("refreshToken", "invalidToken");

                // Act
                var result = await controller.RefreshToken();

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            }

        //     Unit-тести та тест-кейси для методу RevokeToken:



            [TestMethod]
            public async Task RevokeToken_ValidToken_ReturnsOk()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(service => service.RevokeTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.CompletedTask);
                var controller = new AuthController(authServiceMock.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
                controller.Request.Cookies.Append("refreshToken", "validToken");
                var tokenModel = new RevokeTokenRequest { Token = "validToken" };

                // Act
                var result = await controller.RevokeToken(tokenModel);

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                var okResult = (OkObjectResult)result;
                Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            }

            [TestMethod]
            public async Task RevokeToken_InvalidToken_ReturnsBadRequest()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                var controller = new AuthController(authServiceMock.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
                controller.Request.Cookies.Append("refreshToken", "validToken");
                var tokenModel = new RevokeTokenRequest { Token = "invalidToken" };

                // Act
                var result = await controller.RevokeToken(tokenModel);

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            }

        //Unit-тести та тест-кейси для методу Logout:



            [TestMethod]
            public void Logout_ValidToken_ClearsTokenCookie()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                var controller = new AuthController(authServiceMock.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext();

                // Act
                var result = controller.Logout();

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkResult));
                var cookies = controller.Response.Cookies;
                Assert.IsFalse(cookies.ContainsKey("refreshToken"));
            }

        //Unit-тести та тест-кейси для методу GetUserInfo:


            [TestMethod]
            public async Task GetUserInfo_ValidEmail_ReturnsUserData()
            {
                // Arrange
                var authServiceMock = new Mock<IAuthService>();
                authServiceMock.Setup(service => service.GetUserInfo(It.IsAny<string>()))
                    .ReturnsAsync(new UserDataResponse { Email = "test@example.com", Name = "John Doe" });
                var controller = new AuthController(authServiceMock.Object);
                var reqParams = new GetUserInfoRequest { Email = "test@example.com" };

                // Act
                var result = await controller.GetUserInfo(reqParams);

                // Assert
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                var okResult = (OkObjectResult)result;
                Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
                var userData = (UserDataResponse)okResult.Value;
                Assert.AreEqual("test@example.com", userData.Email);
                Assert.AreEqual("John Doe", userData.Name);
            }


        //Unit-тести та тест-кейси для методу ChangeUserInfo:

        [TestMethod]
        public async Task ChangeUserInfo_ValidRequest_ReturnsOk()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(service => service.ChangeUserInfo(It.IsAny<ChangeUserInfoRequest>()))
                .ReturnsAsync(true);
            var controller = new AuthController(authServiceMock.Object);
            var reqParams = new ChangeUserInfoRequest { Name = "John Doe" };

            // Act
            var result = await controller.ChangeUserInfo(reqParams);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

    }
}


//namespace ElnityServerTests
//{
//    public class AuthControllerTests
//    {
//        private readonly Mock<IAuthService> _mockAuthService;
//        private readonly AuthController _controller;

//        public AuthControllerTests()
//        {
//            _mockAuthService = new Mock<IAuthService>();
//            _controller = new AuthController(_mockAuthService.Object);
//        }

//        [Fact]
//        public void Hello_WithAuthorizeAttribute_ReturnsHelloWorldString()
//        {
//            // Arrange
//            _controller.ControllerContext = new ControllerContext();
//            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

//            // Act
//            var result = _controller.Hello();

//            // Assert
//            Assert.Equal("Hello world!", result);
//        }

//        [Fact]
//        public void HelloAdmin_WithAuthorizeAttribute_ReturnsHelloWorldString()
//        {
//            // Arrange
//            _controller.ControllerContext = new ControllerContext();
//            _controller.ControllerContext.HttpContext = new DefaultHttpContext();

//            // Act
//            var result = _controller.HelloAdmin();

//            // Assert
//            Assert.Equal("Hello world!", result);
//        }

//        [Fact]
//        public async Task RegisterAsync_ValidRequest_ReturnsOkResult()
//        {
//            // Arrange
//            var registerRequest = new RegisterRequest();
//            var authenticationResponse = new AuthenticationResponse { InfoMessages = "Ok" };
//            _mockAuthService.Setup(x => x.RegisterAsync(registerRequest)).ReturnsAsync(authenticationResponse);

//            // Act
//            var result = await _controller.RegisterAsync(registerRequest);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task RegisterAsync_InvalidRequest_ReturnsUnprocessableEntityResult()
//        {
//            // Arrange
//            var registerRequest = new RegisterRequest();
//            var authenticationResponse = new AuthenticationResponse { InfoMessages = "Error" };
//            _mockAuthService.Setup(x => x.RegisterAsync(registerRequest)).ReturnsAsync(authenticationResponse);

//            // Act
//            var result = await _controller.RegisterAsync(registerRequest);

//            // Assert
//            Assert.IsType<StatusCodeResult>(result.Result);
//            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
//            Assert.Equal(StatusCodes.Status422UnprocessableEntity, statusCodeResult.StatusCode);
//        }

//        [Fact]
//        public async Task RegisterAsync_ExceptionThrown_ReturnsInternalServerErrorResult()
//        {
//            // Arrange
//            var registerRequest = new RegisterRequest();
//            _mockAuthService.Setup(x => x.RegisterAsync(registerRequest)).ThrowsAsync(new Exception());

//            // Act
//            var result = await _controller.RegisterAsync(registerRequest);

//            // Assert
//            Assert.IsType<StatusCodeResult>(result.Result);
//            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
//            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
//        }

//        [Fact]
//        public async Task LoginAsync_ValidRequest_ReturnsOkResultWithResponse()
//        {
//            // Arrange
//            var loginRequest = new LoginRequest();
//            var authenticationResponse = new AuthenticationResponse { InfoMessages = "Ok" };
//            _mockAuthService.Setup(x => x.LoginAsync(loginRequest, It.IsAny<string>())).ReturnsAsync(authenticationResponse);

//            // Act
//            var result = await _controller.LoginAsync(loginRequest);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            Assert.Equal(authenticationResponse, okResult.Value);
//        }

//        [Fact]
//        public async Task LoginAsync_InvalidRequest_ReturnsUnprocessableEntityResult()
//        {
//            // Arrange
//            var loginRequest = new LoginRequest();
//            var authenticationResponse = new AuthenticationResponse { InfoMessages = "Error" };
//            _mockAuthService.Setup(x => x.LoginAsync(loginRequest, It.IsAny<string>())).ReturnsAsync(authenticationResponse);

//            // Act
//            var result = await _controller.LoginAsync(loginRequest);

//            // Assert
//            Assert.IsType<StatusCodeResult>(result.Result);
//            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
//            Assert.Equal(StatusCodes.Status422UnprocessableEntity, statusCodeResult.StatusCode);
//        }

//        [Fact]
//        public async Task LoginAsync_ExceptionThrown_ReturnsInternalServerErrorResult()
//        {
//            // Arrange
//            var loginRequest = new LoginRequest();
//            _mockAuthService.Setup(x => x.LoginAsync(loginRequest, It.IsAny<string>())).ThrowsAsync(new Exception());

//            // Act
//            var result = await _controller.LoginAsync(loginRequest);

//            // Assert
//            Assert.IsType<StatusCodeResult>(result.Result);
//            var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
//            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
//        }

//        [Fact]
//        public async Task RefreshToken_ValidRequest_ReturnsOkResultWithResponse()
//        {
//            // Arrange
//            _controller.ControllerContext = new ControllerContext();
//            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
//            //_controller.Request.Cookies.Append("refreshToken", "testToken");
//            _controller.Request.Cookies.Append("refreshToken", token, cookieOptions);
//            var refreshDataResponse = new RefreshDataResponse();
//            _mockAuthService.Setup(x => x.RefreshTokenAsync("testToken", It.IsAny<string>())).ReturnsAsync(refreshDataResponse);

//            // Act
//            var result = await _controller.RefreshToken();

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            Assert.Equal(refreshDataResponse, okResult.Value);
//        }

//        [Fact]
//        public async Task RefreshToken_InvalidRequest_ReturnsBadRequestResult()
//        {
//            // Arrange
//            _controller.ControllerContext = new ControllerContext();
//            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
//            //_controller.Request.Cookies.Append("refreshToken", "testToken");
//            _controller.Request.Cookies.Append("refreshToken", token, cookieOptions);
//            var refreshDataResponse = new RefreshDataResponse { RefreshToken = null };
//            _mockAuthService.Setup(x => x.RefreshTokenAsync("testToken", It.IsAny<string>())).ReturnsAsync(refreshDataResponse);

//            // Act
//            var result = await _controller.RefreshToken();

//            // Assert
//            Assert.IsType<BadRequestObjectResult>(result.Result);
//        }

//        [Fact]
//        public async Task RevokeToken_ValidRequest_ReturnsOkResult()
//        {
//            // Arrange
//            var revokeTokenRequest = new RevokeTokenRequest { Token = "testToken" };
//            _mockAuthService.Setup(x => x.RevokeTokenAsync("testToken", It.IsAny<string>())).Returns(Task.CompletedTask);

//            // Act
//            var result = await _controller.RevokeToken(revokeTokenRequest);

//            // Assert
//            Assert.IsType<OkObjectResult>(result);
//        }

//        [Fact]
//        public void Logout_ReturnsOkResult()
//        {
//            // Act
//            var result = _controller.Logout();

//            // Assert
//            Assert.IsType<OkResult>(result);
//        }

//        [Fact]
//        public async Task GetUserInfo_ValidRequest_ReturnsOkResultWithResponse()
//        {
//            // Arrange
//            var getUserInfoRequest = new GetUserInfoRequest { Email = "test@example.com" };
//            var userDataResponse = new UserDataResponse();
//            _mockAuthService.Setup(x => x.GetUserInfo("test@example.com")).ReturnsAsync(userDataResponse);

//            // Act
//            var result = await _controller.GetUserInfo(getUserInfoRequest);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            Assert.Equal(userDataResponse, okResult.Value);
//        }

//        [Fact]
//        public async Task ChangeUserInfo_ValidRequest_ReturnsOkResultWithResponse()
//        {
//            // Arrange
//            var changeUserInfoRequest = new ChangeUserInfoRequest();
//            _mockAuthService.Setup(x => x.ChangeUserInfo(changeUserInfoRequest)).ReturnsAsync(true);

//            // Act
//            var result = await _controller.ChangeUserInfo(changeUserInfoRequest);

//            // Assert
//            Assert.IsType<OkObjectResult>(result.Result);
//            var okResult = Assert.IsType<OkObjectResult>(result.Result);
//            Assert.True((bool)okResult.Value);
//        }
//    }
//}
