using Moq;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using Notes_Dashboard_API.Notes.Services;
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
    public class TestNoteQueryService
    {
        private readonly Mock<INoteRepository> _mock;
        private readonly INoteQueryService _queryNoteNote;

        public TestNoteQueryService()
        {
            _mock = new Mock<INoteRepository>();
            _queryNoteNote = new NoteQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAllNote_ReturnNote()
        {
            var notes = TestNoteFactory.CreateNotes(5);
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(notes);

            var result = await _queryNoteNote.GetAllAsync();

            Assert.Equal(5, result.Count);

        }

        [Fact]
        public async Task GetByIdNote_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((NoteResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryNoteNote.GetByIdAsync(1));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByIdNote_ReturnNote()
        {
            var note = TestNoteFactory.CreateNote(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(note);

            var result = await _queryNoteNote.GetByIdAsync(1);

            Assert.Equal("test1", result.Title);

        }


        [Fact]
        public async Task GetByTitleNote_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByTitleAsync("test")).ReturnsAsync((NoteResponse)null);

            var result = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _queryNoteNote.GetByTitleAsync("test"));

            Assert.Equal(Constants.ItemDoesNotExist, result.Message);

        }

        [Fact]
        public async Task GetByTitleNote_ReturnNote()
        {
            var note = TestNoteFactory.CreateNote(1);
            _mock.Setup(repo => repo.GetByTitleAsync("test1")).ReturnsAsync(note);

            var result = await _queryNoteNote.GetByTitleAsync("test1");

            Assert.Equal("test1", result.Title);

        }
    }
}
