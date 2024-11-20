using Notes_Dashboard_API.Notes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Notes.Helper
{
    public class TestNoteFactory
    {
        public static NoteResponse CreateNote(int id)
        {
            return new NoteResponse
            {
                Id = id,
                Title = "test" + id,
                Description = "asdasdadasadasd",
                Category = "Asdasd",
                CreateDate = "20 aug 2024"
            };
        }

        public static List<NoteResponse> CreateNotes(int count)
        {
            var notes = new List<NoteResponse>();

            for (int i = 0; i < count; i++)
            {
                notes.Add(CreateNote(i));
            }

            return notes;
        }
    }
}
