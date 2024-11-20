using Notes_Dashboard_API.Customers.Dto;

namespace Notes_Dashboard_API.Customers.Services.interfaces
{
    public interface ICustomerQueryService
    {


        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<CustomerResponse> GetByNameAsync(string name);


    }
}
