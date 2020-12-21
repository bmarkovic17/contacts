using System;
using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ContactsApiTests.Fixtures
{
    public class DatabaseFixture : IAsyncDisposable
    {
        public AddressBookContext AddressBookContext { get; private set; }

        public DatabaseFixture()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AddressBookContext>()
                .UseInMemoryDatabase(databaseName: "addressbook")
                .ConfigureWarnings(warningsConfigurationBuilder => warningsConfigurationBuilder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            AddressBookContext = new AddressBookContext(dbContextOptions);

            AddressBookContext.Contacts.Add(new Contact
            {
                Id = 1,
                FirstName = "Keanu",
                Surname = "Reeves",
                DateOfBirth = new DateTime(1964, 9, 2),
                Street = "Linda Ave.",
                AddressNumber = "8106",
                Postcode = "12302",
                City = "Schenectady",
                Country = "New York, US",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 1,
                ContactId = 1,
                ContactDataType = "PHONE",
                ContactDataValue = "0900000000",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 2,
                ContactId = 1,
                ContactDataType = "PHONE",
                ContactDataValue = "0900000001",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 3,
                ContactId = 1,
                ContactDataType = "MAIL",
                ContactDataValue = "keanu.reeves@mail.com",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.Contacts.Add(new Contact
            {
                Id = 2,
                FirstName = "Roger",
                Surname = "Federer",
                DateOfBirth = new DateTime(1981, 8, 8),
                Street = "N. Roehampton Ave.",
                AddressNumber = "7201",
                Postcode = "18042",
                City = "Easton",
                Country = "Pennsylvania, US",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 4,
                ContactId = 2,
                ContactDataType = "PHONE",
                ContactDataValue = "0900000100",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 5,
                ContactId = 2,
                ContactDataType = "MAIL",
                ContactDataValue = "roger.federer@mail.com",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 6,
                ContactId = 2,
                ContactDataType = "MAIL",
                ContactDataValue = "roger.federer@anothermail.com",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.Contacts.Add(new Contact
            {
                Id = 3,
                FirstName = "Mark",
                Surname = "Wahlberg",
                DateOfBirth = new DateTime(1971, 6, 5),
                Street = "Erlenweg",
                AddressNumber = "57",
                Postcode = "3027",
                City = "Bern",
                Country = "Switzerland",
                CreatedOrUpdated = new DateTime(2020, 12, 21)
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 7,
                ContactId = 3,
                ContactDataType = "PHONE",
                ContactDataValue = "0900000200",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 8,
                ContactId = 3,
                ContactDataType = "MAIL",
                ContactDataValue = "mark.wahlberg@mail.com",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.Contacts.Add(new Contact
            {
                Id = 4,
                FirstName = "Superman",
                Street = "Golden Ridge Road",
                AddressNumber = "3357",
                Postcode = "12303",
                City = "Schenectady",
                Country = "New York, US",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 9,
                ContactId = 4,
                ContactDataType = "MAIL",
                ContactDataValue = "superman@mail.com",
                CreatedOrUpdated = DateTime.Now
            });

            AddressBookContext.ContactData.Add(new ContactData
            {
                Id = 10,
                ContactId = 4,
                ContactDataType = "PHONE",
                ContactDataValue = "0900000002",
                CreatedOrUpdated = DateTime.Now
            });

            _ = AddressBookContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public ValueTask DisposeAsync() =>
            AddressBookContext.DisposeAsync();
    }
}
