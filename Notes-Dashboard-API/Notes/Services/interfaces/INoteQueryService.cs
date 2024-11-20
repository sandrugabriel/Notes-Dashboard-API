using Notes_Dashboard_API.Notes.Dto;

namespace Notes_Dashboard_API.Notes.Services.interfaces
{
    public interface INoteQueryService
    {
        Task<List<NoteResponse>> GetAllAsync();

        Task<NoteResponse> GetByIdAsync(int id);

        Task<NoteResponse> GetByTitleAsync(string name);

    }
}
