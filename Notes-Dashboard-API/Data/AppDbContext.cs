using Microsoft.EntityFrameworkCore;
using Notes_Dashboard_API.Customers.Models;
using Notes_Dashboard_API.Notes.Models;
using Notes_Dashboard_API.ServicesNotes.Models;

namespace Notes_Dashboard_API.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<ServicesNote> ServicesNotes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.Property(s => s.Email).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedEmail).HasMaxLength(256);
                entity.Property(s => s.UserName).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedUserName).HasMaxLength(256);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(256);

                entity.HasDiscriminator<string>("Discriminator").HasValue("Customer");

            });

            modelBuilder.Entity<ServicesNote>()
            .HasOne(a => a.Customer)
            .WithMany(a => a.MyNotes)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServicesNote>()
            .HasOne(a => a.Note)
            .WithMany(a => a.Customers)
            .HasForeignKey(a => a.NoteId)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
