using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Database
{
    public class AddressBookContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactData> ContactData { get; set; }

        public AddressBookContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasIndex(contact => new { contact.FirstName, contact.Surname, contact.Street, contact.AddressNumber, contact.Postcode, contact.City, contact.Country })
                .HasDatabaseName("contact_uidx")
                .IsUnique();

            modelBuilder.Entity<ContactData>()
                .HasAlternateKey(contactData => new { contactData.ContactId, contactData.ContactDataType, contactData.ContactDataValue });

            modelBuilder.Entity<ContactData>()
                .HasOne<Contact>()
                .WithMany()
                .HasForeignKey(ContactData => ContactData.ContactId);
        }
    }
}