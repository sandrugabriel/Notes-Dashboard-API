using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Notes_Dashboard_API.ServicesNotes.Models;

namespace Notes_Dashboard_API.Notes.Models
{
    public class Note
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string CreateDate {  get; set; }

        [Required] 
        public string Description {  get; set; }

        [Required]
        public string Category { get; set; }

        public virtual List<ServicesNote> Customers { get; set; }
    }
}
