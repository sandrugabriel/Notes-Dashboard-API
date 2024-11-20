using Notes_Dashboard_API.Customers.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Customers.Helper
{
    public class TestCustomerFactory
    {
        public static CustomerResponse CreateCustomer(int id)
        {
            return new CustomerResponse
            {
                Email = $"test{id}@gmail.com",
                Name = "test" + id,
                PhoneNumber = "077777777" + id
            };
        }

        public static List<CustomerResponse> CreateCustomers(int count)
        {

            List<CustomerResponse> customerResponses = new List<CustomerResponse>();

            for (int i = 0; i < count; i++)
            {
                customerResponses.Add(CreateCustomer(i));
            }

            return customerResponses;
        }
    }
}
