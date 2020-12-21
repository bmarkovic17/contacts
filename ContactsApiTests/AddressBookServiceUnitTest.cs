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

        [Fact]
        public async Task DeleteNonExistingContactAsync()
        {
            // Arrange
            int rowCount = -1;
            var deleteContact = new DeleteContactDto
            {
                Id = 5,
                CreatedOrUpdated = DateTime.Now
            };

            // Act
            try
            {
                rowCount = await _addressBookService.DeleteContactAsync(deleteContact);
            }
            catch (Exception)
            {
            }

            // Assert
            Assert.True(rowCount == -1, "Non existent contact deleted.");
        }

        [Fact]
        public async Task UpdateNonExistingContactAsync()
        {
            // Arrange
            PutContactDto contact = null;
            var putContact = new PutContactDto
            {
                Id = 5,
                FirstName = "Melinda",
                Surname = "Gates",
                DateOfBirth = new DateTime(1955, 10, 28),
                Street = "Pointe Lane",
                AddressNumber = "4597",
                Postcode = "33308",
                City = "Fort Lauderdale",
                Country = "Florida, US",
                CreatedOrUpdated = DateTime.Now
            };

            // Act
            try
            {
                contact = await _addressBookService.PutContactAsync(putContact);
            }
            catch (Exception)
            {
            }

            // Assert
            Assert.True(contact is null, "Non existing contact updated.");
        }

        [Fact]
        public async Task UpdateExistingContactAsync()
        {
            // Arrange
            PutContactDto contact = null;
            var putContact = new PutContactDto
            {
                Id = 3,
                FirstName = "Paul",
                Surname = "Wahlberg",
                DateOfBirth = new DateTime(1971, 6, 5),
                Street = "Erlenweg",
                AddressNumber = "57",
                Postcode = "3027",
                City = "Bern",
                Country = "Switzerland",
                CreatedOrUpdated = new DateTime(2020, 12, 21)
            };

            // Act
            try
            {
                contact = await _addressBookService.PutContactAsync(putContact);
            }
            catch (Exception)
            {
            }

            // Assert
            Assert.True(contact.FirstName == "Paul", "Contact couldn't be updated.");
        }

        [Fact]
        public async Task GetContactDataForNonExistingContactAsync()
        {
            // Arrange
            var contactId = 0;
            List<ContactDataDto> contactData = null;

            // Act
            try
            {
                contactData = await _addressBookService.GetContactDataAsync(contactId);
            }
            catch (Exception)
            {
            }

            // Assert
            Assert.True(contactData is null, "Contact data for non existing client does exist.");
        }

        [Fact]
        public async Task GetContactDataForExistingContactAsync()
        {
            // Arrange
            var contactId = 1;

            // Act
            var contactData = await _addressBookService.GetContactDataAsync(contactId);

            // Assert
            Assert.True(contactData.Count >= 0, "Contact doesn't exist.");
        }
    }
}
