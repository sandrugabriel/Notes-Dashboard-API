using Microsoft.AspNetCore.Mvc;
using Moq;
using Notes_Dashboard_API.Notes.Controllers;
using Notes_Dashboard_API.Notes.Controllers.interfaces;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Services.interfaces;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Notes.Helper;

namespace Tests.Notes.UnitTests
{
    public class TestControllerNote
    {

        private readonly Mock<INoteCommandService> _mockCommandNote;
        private readonly Mock<INoteQueryService> _mockQueryNote;
        private readonly ControllerAPINote noteApiController;

        public TestControllerNote()
        {
            _mockCommandNote = new Mock<INoteCommandService>();
            _mockQueryNote = new Mock<INoteQueryService>();

            noteApiController = new ControllerNote(_mockQueryNote.Object, _mockCommandNote.Object);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var notes = TestNoteFactory.CreateNotes(5);
            _mockQueryNote.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notes);

            var result = await noteApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allNotes = Assert.IsType<List<NoteResponse>>(okResult.Value);

            Assert.Equal(5, allNotes.Count);
            Assert.Equal(200, okResult.StatusCode);

        }


        [Fact]
        public async Task GetById_ItemsDoNotExist()
        {
            _mockQueryNote.Setup(repo => repo.GetByIdAsync(10)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await noteApiController.GetById(10);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var notes = TestNoteFactory.CreateNote(1);
            _mockQueryNote.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(notes);

            var result = await noteApiController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allNotes = Assert.IsType<NoteResponse>(okResult.Value);

            Assert.Equal("test1", allNotes.Title);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task GetBytitle_ItemsDoNotExist()
        {
            _mockQueryNote.Setup(repo => repo.GetByTitleAsync("10")).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var restult = await noteApiController.GetByTitle("10");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemDoesNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetBytitle_ValidData()
        {
            var notes = TestNoteFactory.CreateNote(1);
            _mockQueryNote.Setup(repo => repo.GetByTitleAsync("test1")).ReturnsAsync(notes);

            var result = await noteApiController.GetByTitle("test1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allNotes = Assert.IsType<NoteResponse>(okResult.Value);

            Assert.Equal("test1", allNotes.Title);
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
