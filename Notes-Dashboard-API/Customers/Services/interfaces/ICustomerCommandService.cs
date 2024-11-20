using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Notes.Dto;

namespace Notes_Dashboard_API.Customers.Services.interfaces
{
    public interface ICustomerCommandService
    {
        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddNote(int id, CreateNoteRequest createNoteRequest);

        Task<CustomerResponse> DeleteNote(int id, int noteId);


    }
}
