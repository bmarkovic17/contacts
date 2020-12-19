using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

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


            modelBuilder.Entity<Contact>()
                .HasData(
                    new Contact { Id = 1, FirstName = "Keanu", Surname = "Reeves", DateOfBirth = new DateTime(1964, 9, 2) },
                    new Contact { Id = 2, FirstName = "Roger", Surname = "Federer", DateOfBirth = new DateTime(1981, 8, 8) },
                    new Contact { Id = 3, FirstName = "Mark", Surname = "Wahlberg", DateOfBirth = new DateTime(1971, 6, 5) },
                    new Contact { Id = 4, FirstName = "Superman" },
                    new Contact { Id = 5, FirstName = "Bill", Surname = "Gates", DateOfBirth = new DateTime(1955, 10, 28) }
                );
        }
    }
}