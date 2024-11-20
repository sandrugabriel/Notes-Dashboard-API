using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Notes_Dashboard_API.ServicesNotes.Models;
using Microsoft.AspNetCore.Identity;

namespace Notes_Dashboard_API.Customers.Models
{
    public class Customer : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<ServicesNote> MyNotes { get; set; }
    }
}
