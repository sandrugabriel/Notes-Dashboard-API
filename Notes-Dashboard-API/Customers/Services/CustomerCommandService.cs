using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Repository.interfaces;
using Notes_Dashboard_API.Customers.Services.interfaces;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using Notes_Dashboard_API.System.Constants;
using Notes_Dashboard_API.System.Exceptions;

namespace Notes_Dashboard_API.Customers.Services
{
    public class CustomerCommandService : ICustomerCommandService
    {


        ICustomerRepository _repo;
        INoteRepository _reponote;

        public CustomerCommandService(ICustomerRepository repo, INoteRepository note)
        {
            _repo = repo;
            _reponote = note;
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {
            if (createRequest.Name.Equals("") || createRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            var customer = await _repo.CreateCustomer(createRequest);

            return customer;
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (updateRequest.Name.Equals("") || updateRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            customer = await _repo.UpdateCustomer(id, updateRequest);
            return customer;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeleteCustomer(id);

            return customer;
        }

        public async Task<CustomerResponse> AddNote(int id, CreateNoteRequest createNoteRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (createNoteRequest.Title.Length <= 0) throw new InvalidName(Constants.InvalidName);

            if (createNoteRequest.Description.Length <= 0) throw new InvalidDescription(Constants.InvalidDescription);

            if (createNoteRequest.CreateDate.Length <= 0) throw new InvalidDate(Constants.InvalidDate);

            var note = await _reponote.CreateNote(createNoteRequest);
            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);


            customer = await _repo.AddNote(id, note);

            return customer;
        }

        public async Task<CustomerResponse> DeleteNote(int id, int noteId)
        {
            var customer = await _repo.GetById(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var note = customer.MyNotes.FirstOrDefault(s => s.Note.Id == noteId);

            if (note == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var custresponse = await _repo.GetByIdAsync(id);

            custresponse = await _repo.DeleteNote(id, note);

            return custresponse;
        }

    }
}
