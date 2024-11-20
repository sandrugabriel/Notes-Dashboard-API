using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using Notes_Dashboard_API.Notes.Services.interfaces;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.System.Exceptions;

namespace Notes_Dashboard_API.Notes.Services
{
    public class NoteCommandService : INoteCommandService
    {

        INoteRepository _repo;

        public NoteCommandService(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Note> CreateNote(CreateNoteRequest createRequest)
        {
            if (createRequest.Title.Length <= 0) throw new InvalidName(Constants.InvalidName);

            if (createRequest.Description.Length <= 0) throw new InvalidDescription(Constants.InvalidDescription);

            if (createRequest.CreateDate.Length <= 0) throw new InvalidDate(Constants.InvalidDate);

            return await _repo.CreateNote(createRequest);
        }
        public async Task<NoteResponse> UpdateNote(int id, UpdateNoteRequest updateRequest)
        {
            var note = await _repo.GetByIdAsync(id);
            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);


            if (updateRequest.Title.Length <= 0) throw new InvalidName(Constants.InvalidName);

            if (updateRequest.Description.Length <= 0) throw new InvalidDescription(Constants.InvalidDescription);

            if (updateRequest.CreateDate.Length <= 0) throw new InvalidDate(Constants.InvalidDate);

            note = await _repo.UpdateNote(id, updateRequest);

            return note;
        }

        public async Task<NoteResponse> DeleteNote(int id)
        {
            var note = await _repo.GetByIdAsync(id);
            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteNote(id);

            return note;
        }



    }
}
