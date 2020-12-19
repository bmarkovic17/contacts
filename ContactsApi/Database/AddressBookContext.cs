using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactsApi.Database
{
    public class AddressBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactData> ContactData { get; set; }

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

            modelBuilder.Entity<ContactData>()
                .HasAlternateKey(contactData => new { contactData.ContactId, contactData.ContactDataType, contactData.ContactDataValue });

            modelBuilder.Entity<ContactData>()
                .HasOne<Contact>()
                .WithMany()
                .HasForeignKey(ContactData => ContactData.ContactId);

            modelBuilder.Entity<ContactData>()
                .HasData(
                    new ContactData { Id = 1, ContactId = 1, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0900000000", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 2, ContactId = 1, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0900000001", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 3, ContactId = 1, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "keanu.reeves@mail.com", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 4, ContactId = 2, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0900000100", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 5, ContactId = 2, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "roger.federer@mail.com", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 6, ContactId = 2, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "roger.federer@anothermail.com", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 7, ContactId = 3, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0900000200", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 8, ContactId = 3, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "mark.wahlberg@mail.com", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 9, ContactId = 4, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "superman@mail.com", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 10, ContactId = 4, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0900000002", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 11, ContactId = 5, ContactDataType = "PHONE", ContactDataStatus = "Y", ContactDataValue = "0901000100", CreatedOrUpdated = DateTime.Now },
                    new ContactData { Id = 12, ContactId = 5, ContactDataType = "MAIL", ContactDataStatus = "Y", ContactDataValue = "bill.gates@mail.com", CreatedOrUpdated = DateTime.Now }
                );
        }
    }
}