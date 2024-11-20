using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;

namespace Notes_Dashboard_API.Notes.Repository.interfaces
{
    public interface INoteRepository
    {
        Task<Note> CreateNote(CreateNoteRequest createRequest);

        Task<NoteResponse> DeleteNote(int id);

        Task<List<NoteResponse>> GetAllAsync();

        Task<NoteResponse> GetByIdAsync(int id);

        Task<NoteResponse> GetByTitleAsync(string name);

        Task<Note> GetById(int id);

        Task<Note> GetByTitle(string name);

        Task<NoteResponse> UpdateNote(int id, UpdateNoteRequest updateRequest);
    }
}
