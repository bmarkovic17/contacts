using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using Xunit;

namespace ContactsApiTests
{
    public class ContactsControllerIntegrationTest
    {
        private readonly HttpClient _httpClient;

        public ContactsControllerIntegrationTest() =>
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44329/api/") };

        [Fact]
        public async Task GetAllContactsAsync()
        {
            // Arrange
            var endpoint = "contacts";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetNonExistingContactAsync()
        {
            // Arrange
            var endpoint = "contacts/0";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            string responseValue = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
            Assert.False(string.IsNullOrEmpty(responseValue));
        }

        [Fact]
        public async Task PostNonExistingContactAsync()
        {
            // Arrange
            var endpoint = "contacts";
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
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, postContact);
            ContactDto contact = await response.Content.ReadFromJsonAsync<ContactDto>();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.True(contact.Id > 0);
        }
    }
}
