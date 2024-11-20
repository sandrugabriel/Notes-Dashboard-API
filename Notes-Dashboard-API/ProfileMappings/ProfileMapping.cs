using AutoMapper;
using Notes_Dashboard_API.Customers.Dto;
using Notes_Dashboard_API.Customers.Models;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.ServicesNotes.Models;

namespace Notes_Dashboard_API.ProfileMappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping() {

            CreateMap<CreateNoteRequest, Note>();
            CreateMap<Note, NoteResponse>();
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
            CreateMap<ServicesNote, NoteResponse>().ForMember(s => s.Id, op => op.MapFrom(sr => sr.Note.Id))
                .ForMember(s => s.CreateDate, op => op.MapFrom(sr => sr.Note.CreateDate))
                .ForMember(s => s.Category, op => op.MapFrom(sr => sr.Note.Category))
                .ForMember(s => s.Title, op => op.MapFrom(sr => sr.Note.Title))
                .ForMember(s => s.Description, op => op.MapFrom(sr => sr.Note.Description));
        }
    }
}
