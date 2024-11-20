using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Models;
using Notes_Dashboard_API.Customers.Repository.interfaces;
using Notes_Dashboard_API.Data;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.ServicesNotes.Models;

namespace Notes_Dashboard_API.Customers.Repository
{
    public class CustomerRepository: ICustomerRepository
    {

        AppDbContext _context;
        IMapper _mapper;

        public CustomerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(c => c.Id == id);
         //   var test = _mapper.Map<NoteResponse>(customer.MyNotes[0]);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {

            var customer = _mapper.Map<Customer>(createRequest);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }
        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(s => s.Id == id);
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.Name = updateRequest.Name ?? customer.Name;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(s => s.Id == id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> AddNote(int id, Note note)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(s => s.Id == id);

            var servicenote = new ServicesNote();
            servicenote.Customer = customer;
            servicenote.CustomerId = id;
            servicenote.Note = note;
            servicenote.NoteId = note.Id;


            var serviceResponse = _mapper.Map<NoteResponse>(servicenote);

            customer.MyNotes.Add(servicenote);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            //customerResponse.MyNotes.Add(serviceResponse);

            return customerResponse;
        }

        public async Task<CustomerResponse> DeleteNote(int id, ServicesNote servicenote)
        {
            var customer = await _context.Customers.Include(s => s.MyNotes).ThenInclude(s=>s.Note).FirstOrDefaultAsync(s => s.Id == id);

            customer.MyNotes.Remove(servicenote);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

    }
}
