using Notes_Dashboard_API.Notes.Dto;

namespace Notes_Dashboard_API.Customers.Dto
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        public List<NoteResponse> MyNotes { get; set; }


    }
}
