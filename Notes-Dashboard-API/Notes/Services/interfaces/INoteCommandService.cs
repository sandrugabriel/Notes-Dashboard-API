using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;

namespace Notes_Dashboard_API.Notes.Services.interfaces
{
    public interface INoteCommandService
    {
        Task<Note> CreateNote(CreateNoteRequest createRequest);

        Task<NoteResponse> UpdateNote(int id, UpdateNoteRequest updateRequest);

        Task<NoteResponse> DeleteNote(int id);
    }
}
