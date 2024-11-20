using Moq;
using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Repository.interfaces;
using Notes_Dashboard_API.Customers.Services.interfaces;
using Notes_Dashboard_API.Customers.Services;
using Notes_Dashboard_API.System.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Customers.Helper;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.Notes.Dto;

namespace Tests.Customers.UnitTests
{
    public class TestCustomerCommandService
    {
        private readonly Mock<ICustomerRepository> _mock;
        private readonly Mock<INoteRepository> _mockNote;

        private readonly ICustomerCommandService _commandNote;

        public TestCustomerCommandService()
        {
            _mock = new Mock<ICustomerRepository>();
            _mockNote = new Mock<INoteRepository>();
            _commandNote = new CustomerCommandService(_mock.Object, _mockNote.Object);

        }

        [Fact]
        public async Task CreateCustomer_InvalidName()
        {
            var createRequest = new CreateCustomerRequest
            {
                Name = "",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.CreateCustomer(createRequest)).ReturnsAsync((CustomerResponse)null);
            Exception exception = await Assert.ThrowsAsync<InvalidName>(() => _commandNote.CreateCustomer(createRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task CreateCustomer_ReturnCustomer()
        {
            var createRequest = new CreateCustomerRequest
            {
                Name = "test50",
                Password = "1234",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(50);

            _mock.Setup(repo => repo.CreateCustomer(It.IsAny<CreateCustomerRequest>())).ReturnsAsync(customer);

            var result = await _commandNote.CreateCustomer(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Name, createRequest.Name);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Name = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((CustomerResponse)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandNote.UpdateCustomer(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidName()
        {
            var updateRequest = new UpdateCustomerRequest
            {

                Name = "",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);
            customer.Name = updateRequest.Name;
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            var exception = await Assert.ThrowsAsync<InvalidName>(() => _commandNote.UpdateCustomer(1, updateRequest));

            Assert.Equal(Constants.InvalidName, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData_ReturnCustomer()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Name = "Test",
                PhoneNumber = "07777777",
                Email = "test@gmail.com"
            };

            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mock.Setup(repo => repo.UpdateCustomer(It.IsAny<int>(), It.IsAny<UpdateCustomerRequest>())).ReturnsAsync(customer);

            var result = await _commandNote.UpdateCustomer(1, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(customer, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteCustomer(It.IsAny<int>())).ReturnsAsync((CustomerResponse)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandNote.DeleteCustomer(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            var customer = TestCustomerFactory.CreateCustomer(1);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var restul = await _commandNote.DeleteCustomer(1);

            Assert.NotNull(restul);
            Assert.Equal(customer, restul);
        }


        [Fact]
        public async Task AddNote_ValidData()
        {
            var createRequest = new CreateNoteRequest
            {
                Category = "social",
                CreateDate = "20 aug 2024",
                Description = "Description",
                Title = "Title"
            };

            var customer = TestCustomerFactory.CreateCustomer(50);

            Note note = new Note
            {
                Id = 1,
                Category = "social",
                CreateDate = "20 aug 2024",
                Description = "Description",
                Title = "Title"
            };
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
            _mockNote.Setup(repo => repo.CreateNote(It.IsAny<CreateNoteRequest>())).ReturnsAsync(note);
            _mock.Setup(repo => repo.AddNote(It.IsAny<int>(), note)).ReturnsAsync(customer);

            var result = await _commandNote.AddNote(50,createRequest);

            Assert.NotNull(result);
            Assert.Equal(customer.Name, result.Name);
        }

        [Fact]
        public async Task AddNote_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.AddNote(It.IsAny<int>(), It.IsAny<Note>())).ReturnsAsync((CustomerResponse)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandNote.DeleteCustomer(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }


    }
}
