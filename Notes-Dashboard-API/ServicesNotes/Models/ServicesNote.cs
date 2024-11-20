using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.Customers.Models;

namespace Notes_Dashboard_API.ServicesNotes.Models
{
    [Table("NotesServices")]
    public class ServicesNote
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [ForeignKey("NoteId")]
        public int NoteId { get; set; }

        [JsonIgnore]
        public virtual Note Note { get; set; }
    }
}
