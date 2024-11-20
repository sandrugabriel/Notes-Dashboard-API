using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using Notes_Dashboard_API.Notes.Services.interfaces;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.System.Exceptions;

namespace Notes_Dashboard_API.Notes.Services
{
    public class NoteQueryService : INoteQueryService
    {

        INoteRepository _repo;

        public NoteQueryService(INoteRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<NoteResponse>> GetAllAsync()
        {
            var note = await _repo.GetAllAsync();
            if (note.Count == 0) return new List<NoteResponse>();
            return note.ToList();
        }

        public async Task<NoteResponse> GetByIdAsync(int id)
        {
            var note = await _repo.GetByIdAsync(id);
            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return note;
        }

        public async Task<NoteResponse> GetByTitleAsync(string name)
        {
            var note = await _repo.GetByTitleAsync(name);
            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            return note;
        }



    }
}
