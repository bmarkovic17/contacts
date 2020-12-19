using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactsApi.Database
{
    public class AddressBookContext : DbContext
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
                .HasIndex(contact => new { contact.FirstName, contact.Surname, contact.Street, contact.AddressNumber, contact.Postcode, contact.City, contact.Country })
                .HasDatabaseName("contact_uidx")
                .IsUnique();

            modelBuilder.Entity<Contact>()
                .HasData(
                    new Contact { Id = 1, FirstName = "Keanu", Surname = "Reeves", DateOfBirth = new DateTime(1964, 9, 2), Street = "Linda Ave.", AddressNumber = "8106", Postcode = "12302", City = "Schenectady", Country = "New York, US", CreatedOrUpdated = DateTime.Now },
                    new Contact { Id = 2, FirstName = "Roger", Surname = "Federer", DateOfBirth = new DateTime(1981, 8, 8), Street = "N. Roehampton Ave.", AddressNumber = "7201", Postcode = "18042", City = "Easton", Country = "Pennsylvania, US", CreatedOrUpdated = DateTime.Now },
                    new Contact { Id = 3, FirstName = "Mark", Surname = "Wahlberg", DateOfBirth = new DateTime(1971, 6, 5), Street = "Erlenweg", AddressNumber = "57", Postcode = "3027", City = "Bern", Country = "Switzerland", CreatedOrUpdated = DateTime.Now },
                    new Contact { Id = 4, FirstName = "Superman", Street = "Golden Ridge Road", AddressNumber = "3357", Postcode = "12303", City = "Schenectady", Country = "New York, US", CreatedOrUpdated = DateTime.Now },
                    new Contact { Id = 5, FirstName = "Bill", Surname = "Gates", DateOfBirth = new DateTime(1955, 10, 28), Street = "Pointe Lane", AddressNumber = "4597", Postcode = "33308", City = "Fort Lauderdale", Country = "Florida, US", CreatedOrUpdated = DateTime.Now }
                );
        }
    }
}