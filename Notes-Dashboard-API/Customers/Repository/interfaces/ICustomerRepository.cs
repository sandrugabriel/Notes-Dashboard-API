using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Models;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.ServicesNotes.Models;

namespace Notes_Dashboard_API.Customers.Repository.interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<Customer> GetById(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddNote(int id, Note note);

        Task<CustomerResponse> DeleteNote(int id, ServicesNote servicesNote);
    }
}
