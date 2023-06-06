//using AutoMapper;
//using ElnityServerBLL.Dto.App.Request;
//using ElnityServerBLL.Dto.App.Response;
//using ElnityServerBLL.Services.Implementation;
//using ElnityServerBLL.Services.Interfaces;
//using ElnityServerDAL.Constant;
//using ElnityServerDAL.Context;
//using ElnityServerDAL.Entities.App;
//using ElnityServerDAL.Entities.Identity;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;

//namespace ElnityServerTests.Services
//{
//    public class AppServiceTests
//    {
//        private readonly AppService _appService;
//        private readonly Mock<ApplicationDbContext> _dbContextMock;
//        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
//        private readonly AppEnvironment _appEnvironment;

//        public AppServiceTests()
//        {
//            _dbContextMock = new Mock<ApplicationDbContext>();
//            _userManagerMock = GetUserManagerMock();

//            var appEnvironmentOptions = Options.Create(new AppEnvironment());
//            _appEnvironment = appEnvironmentOptions.Value;

//            _appService = new AppService(
//                _dbContextMock.Object,
//                _userManagerMock.Object,
//                appEnvironmentOptions
//            );
//        }

//        private Mock<UserManager<ApplicationUser>> GetUserManagerMock()
//        {
//            var store = new Mock<IUserStore<ApplicationUser>>();
//            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
//        }

//        [Fact]
//        public async Task ChangeAppStatus_ValidRequest_ReturnsTrue()
//        {
//            // Arrange
//            var reqParams = new ChangeAppStatusRequest
//            {
//                AppId = Guid.NewGuid(),
//                NewDevelopingStatus = true
//            };
//            var existingApp = new App
//            {
//                Id = reqParams.AppId,
//                // Set other properties as needed
//            };

//            var mockDbSet = new Mock<DbSet<App>>();
//            mockDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync(existingApp);
//            _dbContextMock.Setup(x => x.Apps).Returns(mockDbSet.Object);

//            // Act
//            var result = await _appService.ChangeAppStatus(reqParams);

//            // Assert
//            Assert.True(result);
//            Assert.Equal(reqParams.NewDevelopingStatus, existingApp.InDevelop);
//            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
//        }

//        [Fact]
//        public async Task AddNewAppAsync_ValidRequest_ReturnsTrue()
//        {
//            // Arrange
//            var newApp = new AddNewAppRequest
//            {
//                Name = "Test App",
//                Description = "Test App Description",
//                Image = new FormFile(null, 0, 0, "Test Image", "test.jpg")
//            };
//            var appExist = (App)null;

//            var mockDbSet = new Mock<DbSet<App>>();
//            mockDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync(appExist);
//            _dbContextMock.Setup(x => x.Apps).Returns(mockDbSet.Object);

//            // Act
//            var result = await _appService.AddNewAppAsync(newApp);

//            // Assert
//            Assert.True(result);
//            _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
//        }

//        //[Fact]
//        //public async Task GetAllAppsAsync_ReturnsAllApps()
//        //{
//        //    // Arrange
//        //    var apps = new List<App>
//        //    {
//        //        new App { Id = Guid.NewGuid(), Name = "App 1" },
//        //        new App { Id = Guid.NewGuid(), Name = "App 2" },
//        //        new App { Id = Guid.NewGuid(), Name = "App 3" }
//        //    };
//        //    var mockDbSet = new Mock<DbSet<App>>();
//        //    mockDbSet.As<IAsyncEnumerable<App>>().Setup(x => x.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<App>(apps.GetEnumerator()));
//        //    Moq.Language.Flow.IReturnsResult<IQueryable<App>> returnsResult = mockDbSet.As<IQueryable<App>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<App>(apps.Provider));
//        //    mockDbSet.As<IQueryable<App>>().Setup(x => x.Expression).Returns(apps.AsQueryable().Expression);
//        //    mockDbSet.As<IQueryable<App>>().Setup(x => x.ElementType).Returns(apps.AsQueryable().ElementType);
//        //    mockDbSet.As<IQueryable<App>>().Setup(x => x.GetEnumerator()).Returns(apps.GetEnumerator());
//        //    _dbContextMock.Setup(x => x.Apps).Returns(mockDbSet.Object);

//        //    // Act
//        //    var result = await _appService.GetAllAppsAsync();

//        //    // Assert
//        //    Assert.Equal(apps.Count, result.Count);
//        //    for (int i = 0; i < apps.Count; i++)
//        //    {
//        //        Assert.Equal(apps[i].Id, result[i].Id);
//        //        Assert.Equal(apps[i].Name, result[i].Name);
//        //        // Assert other properties as needed
//        //    }
//        //}

//        //[Fact]
//        //public async Task GetAppPreviewAsync_ValidRequest_ReturnsAppPreviewResponse()
//        //{
//        //    // Arrange
//        //    var reqParams = new GetAppPreviewRequest
//        //    {
//        //        Email = "test@example.com",
//        //        AppId = Guid.NewGuid()
//        //    };
//        //    var user = new ApplicationUser { Id = Guid.NewGuid().ToString() };
//        //    var app = new App
//        //    {
//        //        Id = reqParams.AppId,
//        //        // Set other properties as needed
//        //    };
//        //    var checkUser = (JournalApp)null;

//        //    _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//        //    var mockDbSet = new Mock<DbSet<App>>();
//        //    mockDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync(app);
//        //    _dbContextMock.Setup(x => x.Apps).Returns(mockDbSet.Object);

//        //    var mockJournalUserApps = new Mock<DbSet<JournalApp>>();
//        //    mockJournalUserApps.Setup(x => x.Where(It.IsAny<System.Linq.Expressions.Expression<Func<JournalApp, bool>>>()))
//        //        .Returns((IEnumerable<JournalApp>)null);
//        //    _dbContextMock.Setup(x => x.JournalUserApps).Returns(mockJournalUserApps.Object);

//        //    // Act
//        //    var result = await _appService.GetAppPreviewAsync(reqParams);

//        //    // Assert
//        //    Assert.Equal(app.Name, result.Name);
//        //    Assert.Equal(app.Description, result.Description);
//        //    Assert.Equal(app.ImagePath, result.ImagePath);
//        //    Assert.Equal(app.InDevelop, result.InDevelop);
//        //    Assert.False(result.CurrentUserAlredy);
//        //}

//        //[Fact]
//        //public async Task RegisterAppAsync_ValidRequest_ReturnsTrue()
//        //{
//        //    // Arrange
//        //    var reqParams = new RegisterAppRequest
//        //    {
//        //        Email = "test@example.com",
//        //        AppId = Guid.NewGuid()
//        //    };
//        //    var user = new ApplicationUser { Id = Guid.NewGuid().ToString() };
//        //    var app = new App { Id = reqParams.AppId };

//        //    _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//        //    var mockDbSet = new Mock<DbSet<App>>();
//        //    mockDbSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).ReturnsAsync(app);
//        //    _dbContextMock.Setup(x => x.Apps).Returns(mockDbSet.Object);

//        //    // Act
//        //    var result = await _appService.RegisterAppAsync(reqParams);

//        //    // Assert
//        //    Assert.True(result);
//        //    _dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
//        //}

//        //[Fact]
//        //public async Task GetUserAppsAsync_ValidRequest_ReturnsUserAppResponseList()
//        //{
//        //    // Arrange
//        //    var reqParams = new GetUserAppsRequest
//        //    {
//        //        Email = "test@example.com"
//        //    };
//        //    var user = new ApplicationUser { Id = Guid.NewGuid().ToString() };
//        //    var app1 = new App { Id = Guid.NewGuid(), Name = "App 1" };
//        //    var app2 = new App { Id = Guid.NewGuid(), Name = "App 2" };
//        //    var app3 = new App { Id = Guid.NewGuid(), Name = "App 3" };
//        //    var journalApp1 = new JournalApp { UserId = user.Id, AppId = app1.Id, App = app1 };
//        //    var journalApp2 = new JournalApp { UserId = user.Id, AppId = app2.Id, App = app2 };

//        //    _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//        //    var mockJournalUserApps = new Mock<DbSet<JournalApp>>();
//        //    mockJournalUserApps.As<IAsyncEnumerable<JournalApp>>().Setup(x => x.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<JournalApp>(new List<JournalApp> { journalApp1, journalApp2 }.GetEnumerator()));
//        //    mockJournalUserApps.As<IQueryable<JournalApp>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<JournalApp>(new List<JournalApp> { journalApp1, journalApp2 }.Provider));
//        //    mockJournalUserApps.As<IQueryable<JournalApp>>().Setup(x => x.Expression).Returns(new List<JournalApp> { journalApp1, journalApp2 }.AsQueryable().Expression);
//        //    mockJournalUserApps.As<IQueryable<JournalApp>>().Setup(x => x.ElementType).Returns(new List<JournalApp> { journalApp1, journalApp2 }.AsQueryable().ElementType);
//        //    mockJournalUserApps.As<IQueryable<JournalApp>>().Setup(x => x.GetEnumerator()).Returns(new List<JournalApp> { journalApp1, journalApp2 }.GetEnumerator());
//        //    _dbContextMock.Setup(x => x.JournalUserApps).Returns(mockJournalUserApps.Object);

//        //    // Act
//        //    var result = await _appService.GetUserAppsAsync(reqParams);

//        //    // Assert
//        //    Assert.Equal(2, result.Count());
//        //    var resultList = result.ToList();
//        //    Assert.Equal(app1.Id, resultList[0].Id);
//        //    Assert.Equal(app1.Name, resultList[0].Name);
//        //    // Assert other properties as needed
//        //    Assert.Equal(app2.Id, resultList[1].Id);
//        //    Assert.Equal(app2.Name, resultList[1].Name);
//        //    // Assert other properties as needed
//        //}
//    }
//}
