namespace Notes_Dashboard_API.Notes.Dto
{
    public class UpdateNoteRequest
    {
        public string? Title { get; set; }

        public string? CreateDate { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
    }
}
