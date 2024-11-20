using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Repository.interfaces;
using Notes_Dashboard_API.Customers.Services.interfaces;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.System.Exceptions;

namespace Notes_Dashboard_API.Customers.Services
{
    public class CustomerQueryService : ICustomerQueryService
    {


        ICustomerRepository _repo;

        public CustomerQueryService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _repo.GetAllAsync();
            if (customers.Count == 0) return new List<CustomerResponse>();

            return customers;
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }



    }
}
