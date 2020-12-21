using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using ContactsApi.Repositories.Implementations;
using ContactsApi.Services.Implementations;
using ContactsApi.Services.Interfaces;
using ContactsApiTests.Fixtures;
using Xunit;

namespace ContactsApiTests
{
    [Collection("Automapper collection")]
    public class AddressBookServiceUnitTest : IClassFixture<DatabaseFixture>
    {
        private readonly IAddressBookService _addressBookService;

        public AddressBookServiceUnitTest(DatabaseFixture databaseFixture, AutoMapperFixture autoMapperFixture) =>
            _addressBookService = new AddressBookService(
                new AddressBookDatabase(databaseFixture.AddressBookContext),
                new ContactsRepository(databaseFixture.AddressBookContext),
                new ContactDataRepository(databaseFixture.AddressBookContext),
                autoMapperFixture.Mapper);

        [Fact]
        public async Task GetAllContactsAsync()
        {
            // Act
            List<ContactDto> contacts = await _addressBookService.GetContactsAsync(null);

            // Assert
            Assert.True(contacts.Any(), "There isn't any contacts in the address book");
        }

        [Fact]
        public async Task GetExistingContactAsync()
        {
            // Arrange
            var id = 1;

            // Act
            ContactDto contact = (await _addressBookService.GetContactsAsync(id)).Single();

            // Assert
            Assert.True(contact.Id == 1, "Contact with ID {id} doesn't exist.");
        }

        [Fact]
        public async Task GetNonExistingContactAsync()
        {
            // Arrange
            var id = 0;

            // Act
            List<ContactDto> contacts = await _addressBookService.GetContactsAsync(id);

            // Assert
            Assert.True(contacts.Count == 0, $"There is a contact with ID {id}.");
        }

        [Fact]
        public async Task PostNonExistingContactAsync()
        {
            // Arrange
            var postContact = new PostContactDto
            {
                FirstName = "Bill",
                Surname = "Gates",
                DateOfBirth = new DateTime(1955, 10, 28),
                Street = "Pointe Lane",
                AddressNumber = "4597",
                Postcode = "33308",
                City = "Fort Lauderdale",
                Country = "Florida, US",
                ContactData = new List<PostContactDataDto>
                {
                    new PostContactDataDto
                    {
                        ContactDataType = "PHONE",
                        ContactDataValue = "0901000100"
                    },
                    new PostContactDataDto
                    {
                        ContactDataType = "MAIL",
                        ContactDataValue = "bill.gates@mail.com"
                    }
                }
            };

            // Act
            ContactDto contact = await _addressBookService.PostContactAsync(postContact);

            // Assert
            Assert.False(contact is null, "Contact couldn't be created.");
        }
    }
}
