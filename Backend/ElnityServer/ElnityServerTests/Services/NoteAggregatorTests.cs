//using ElnityServerBLL.Dto.NoteAggregator.Request;
//using ElnityServerBLL.Dto.NoteAggregator.Response;
//using ElnityServerBLL.Services.Implementation;
//using ElnityServerDAL.Constant;
//using ElnityServerDAL.Context;
//using ElnityServerDAL.Entities.App;
//using ElnityServerDAL.Entities.Identity;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Moq;


//namespace ElnityServerBLL.Tests.Services
//{
//    public class NoteAggregatorTests
//    {
//        private readonly NoteAggregator _noteAggregator;
//        private readonly Mock<ApplicationDbContext> _dbContextMock;
//        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
//        private readonly Mock<IOptions<AppEnvironment>> _appEnvironmentMock;

//        public NoteAggregatorTests()
//        {
//            _dbContextMock = new Mock<ApplicationDbContext>();
//            _userManagerMock = MockUserManager<ApplicationUser>();
//            _appEnvironmentMock = new Mock<IOptions<AppEnvironment>>();
//            _noteAggregator = new NoteAggregator(_dbContextMock.Object, _userManagerMock.Object, _appEnvironmentMock.Object);
//        }

//        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
//        {
//            var userStoreMock = new Mock<IUserStore<TUser>>();
//            return new Mock<UserManager<TUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
//        }

//        [Fact]
//        public async Task AddNoteAsync_ValidRequest_ReturnsTrue()
//        {
//            // Arrange
//            var reqParams = new AddNoteRequest
//            {
//                Email = "test@example.com",
//                Image = new FormFile(Stream.Null, 0, 0, "image", "image.jpg")
//            };
//            var user = new ApplicationUser
//            {
//                Id = Guid.NewGuid(),
//                BaseRoot = "/root"
//            };

//            _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//            _dbContextMock.Setup(x => x.NoteAppUserFields.Add(It.IsAny<NoteApp>())).Verifiable();
//            _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

//            // Act
//            var result = await _noteAggregator.AddNoteAsync(reqParams);

//            // Assert
//            Assert.True(result);
//            _dbContextMock.Verify(x => x.NoteAppUserFields.Add(It.IsAny<NoteApp>()), Times.Once);
//            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
//        }

//        [Fact]
//        public async Task AddNoteAsync_InvalidRequest_ReturnsFalse()
//        {
//            // Arrange
//            var reqParams = new AddNoteRequest
//            {
//                Email = "test@example.com",
//                Image = null // Invalid request
//            };

//            // Act
//            var result = await _noteAggregator.AddNoteAsync(reqParams);

//            // Assert
//            Assert.False(result);
//            _dbContextMock.Verify(x => x.NoteAppUserFields.Add(It.IsAny<NoteApp>()), Times.Never);
//            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
//        }

//        [Fact]
//        public async Task DeleteNoteAsync_ValidRequest_ReturnsTrue()
//        {
//            // Arrange
//            var reqParams = new DeleteNoteRequest
//            {
//                Id = Guid.NewGuid()
//            };
//            var note = new NoteApp { Id = reqParams.Id };

//            _dbContextMock.Setup(x => x.NoteAppUserFields.Where(p => p.Id == reqParams.Id)).Returns(MockDbSet(note));
//            _dbContextMock.Setup(x => x.Remove(note)).Verifiable();
//            _dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);

//            // Act
//            var result = await _noteAggregator.DeleteNoteAsync(reqParams);

//            // Assert
//            Assert.True(result);
//            _dbContextMock.Verify(x => x.NoteAppUserFields.Where(p => p.Id == reqParams.Id), Times.Once);
//            _dbContextMock.Verify(x => x.Remove(note), Times.Once);
//            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteNoteAsync_InvalidRequest_ReturnsFalse()
//        {
//            // Arrange
//            var reqParams = new DeleteNoteRequest
//            {
//                Id = Guid.NewGuid() // Non-existing note
//            };

//            // Act
//            var result = await _noteAggregator.DeleteNoteAsync(reqParams);

//            // Assert
//            Assert.False(result);
//            _dbContextMock.Verify(x => x.NoteAppUserFields.Where(p => p.Id == reqParams.Id), Times.Once);
//            _dbContextMock.Verify(x => x.Remove(It.IsAny<NoteApp>()), Times.Never);
//            _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
//        }

//        [Fact]
//        public async Task GetNoteAsync_ValidRequest_ReturnsUserNoteResponseList()
//        {
//            // Arrange
//            var reqParams = new GetNoteUserRequest
//            {
//                Email = "test@example.com"
//            };
//            var user = new ApplicationUser
//            {
//                Id = Guid.NewGuid(),
 
//            };
//            var note1 = new NoteApp { Id = Guid.NewGuid(), UserId = user.Id, Name = "Note 1" };
//            var note2 = new NoteApp { Id = Guid.NewGuid(), UserId = user.Id, Name = "Note 2" };
//            var note3 = new NoteApp { Id = Guid.NewGuid(), UserId = user.Id, Name = "Note 3" };

//            _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//            _dbContextMock.Setup(x => x.NoteAppUserFields.Where(p => p.UserId == user.Id)).Returns(MockDbSet(note1, note2, note3));

//            // Act
//            var result = await _noteAggregator.GetNoteAsync(reqParams);

//            // Assert
//            Assert.Equal(3, result.Count());
//            var resultList = result.ToList();
//            Assert.Equal(note1.Id, resultList[0].Id);
//            Assert.Equal(note1.Name, resultList[0].Name);
//            // Assert other properties as needed
//            Assert.Equal(note2.Id, resultList[1].Id);
//            Assert.Equal(note2.Name, resultList[1].Name);
//            // Assert other properties as needed
//            Assert.Equal(note3.Id, resultList[2].Id);
//            Assert.Equal(note3.Name, resultList[2].Name);
//            // Assert other properties as needed
//        }

//        //[Fact]
//        //public async Task GetNoteAsync_InvalidRequest_ReturnsEmptyUserNoteResponseList()
//        //{
//        //    // Arrange
//        //    var reqParams = new GetNoteUserRequest
//        //    {
//        //        Email = "test@example.com"
//        //    };
//        //    var user = new ApplicationUser
//        //    {
//        //        Id = Guid.NewGuid(),
//        //        BaseRoot = "/root"
//        //    };

//        //    _userManagerMock.Setup(x => x.FindByEmailAsync(reqParams.Email)).ReturnsAsync(user);

//        //    // Act
//        //    var result = await _noteAggregator.GetNoteAsync(reqParams);

//        //    // Assert
//        //    Assert.Empty(result);
//        //    _dbContextMock.Verify(x => x.NoteAppUserFields.Where(p => p.UserId == user.Id), Times.Once);
//        //}

//        private DbSet<T> MockDbSet<T>(params T[] entities) where T : class
//        {
//            var queryable = entities.AsQueryable();

//            var dbSetMock = new Mock<DbSet<T>>();
//            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryable.Provider);
//            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryable.Expression);
//            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
//            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

//            return dbSetMock.Object;
//        }

//        // Add unit tests for other helper methods if needed
//    }
//}
