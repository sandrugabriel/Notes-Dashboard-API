using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes_Dashboard_API.Data;
using Notes_Dashboard_API.Notes.Dto;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.Notes.Repository.interfaces;
using System;

namespace Notes_Dashboard_API.Notes.Repository
{
    public class NoteRepository : INoteRepository
    {

        AppDbContext _context;
        IMapper _mapper;

        public NoteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Note> CreateNote(CreateNoteRequest createRequest)
        {

            var note = _mapper.Map<Note>(createRequest);

            _context.Notes.Add(note);

            await _context.SaveChangesAsync();

            return note;

        }

        public async Task<NoteResponse> DeleteNote(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Id == id);

            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<List<NoteResponse>> GetAllAsync()
        {
            List<Note> notes = await _context.Notes.ToListAsync();

            return _mapper.Map<List<NoteResponse>>(notes);
        }

        public async Task<NoteResponse> GetByIdAsync(int id)
        {

            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<NoteResponse> GetByTitleAsync(string name)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Title == name);

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<Note> GetById(int id)
        {

            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Id == id);

            return note;
        }

        public async Task<Note> GetByTitle(string name)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Title == name);

            return note;
        }


        public async Task<NoteResponse> UpdateNote(int id, UpdateNoteRequest updateRequest)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(s => s.Id == id);

            note.Title = updateRequest.Title ?? note.Title;
            note.CreateDate = updateRequest.CreateDate ?? note.CreateDate;
            note.Description = updateRequest.Description ?? note.Description;   
            note.Category = updateRequest.Category ?? note.Category;    

            _context.Notes.Update(note);

            await _context.SaveChangesAsync();


            return _mapper.Map<NoteResponse>(note);
        }




    }
}
