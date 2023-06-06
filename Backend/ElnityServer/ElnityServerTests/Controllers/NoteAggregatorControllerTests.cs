using ElnityServer.Authorization.CustomAttributes;
using ElnityServer.Controllers;
using ElnityServerBLL.Dto.App.Request;
using ElnityServerBLL.Dto.NoteAggregator.Request;
using ElnityServerBLL.Dto.NoteAggregator.Response;
using ElnityServerBLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ElnityServerTests.Controllers
{
    public class NoteAggregatorControllerTests
    {
        private readonly NoteAggregatorController _controller;
        private readonly Mock<INoteAggregator> _noteAggregatorMock;

        public NoteAggregatorControllerTests()
        {
            _noteAggregatorMock = new Mock<INoteAggregator>();
            _controller = new NoteAggregatorController(_noteAggregatorMock.Object);
        }

        [Fact]
        public async Task AddNote_ReturnsOkResult()
        {
            // Arrange
            var request = new AddNoteRequest(); // Add your test request object here
            _noteAggregatorMock.Setup(x => x.AddNoteAsync(It.IsAny<AddNoteRequest>())).ReturnsAsync(true);

            // Act
            var result = await _controller.AddNote(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetNote_ReturnsOkResult()
        {
            // Arrange
            var request = new GetNoteUserRequest(); // Add your test request object here
            var expectedResponse = new List<UserNoteResponse>(); // Add your expected response object here
            _noteAggregatorMock.Setup(x => x.GetNoteAsync(It.IsAny<GetNoteUserRequest>())).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.GetNote(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteNote_ReturnsOkResult()
        {
            // Arrange
            var request = new DeleteNoteRequest(); // Add your test request object here
            _noteAggregatorMock.Setup(x => x.DeleteNoteAsync(It.IsAny<DeleteNoteRequest>())).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteNote(request);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
