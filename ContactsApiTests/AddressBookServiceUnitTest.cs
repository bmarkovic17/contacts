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
    public class AddressBookServiceUnitTest : IClassFixture<DatabaseFixture>, IClassFixture<AutoMapperFixture>
    {
        private readonly IAddressBookService _addressBookService;

        public AddressBookServiceUnitTest(DatabaseFixture databaseFixture, AutoMapperFixture autoMapperFixture) =>
            _addressBookService = new AddressBookService(
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
    }
}
