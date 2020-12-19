using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Database
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        // I had problems with migrations when I used a constructor with a parameter so for now the connection
        // string is hardcoded here
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
            dbContextOptionsBuilder
                .UseNpgsql("Host=localhost;Database=contacts;Username=dba;Password=dba123!")
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(contact => new { contact.FirstName, contact.Surname })
                .HasDatabaseName("contact_uidx");
        }
    }
}