using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContactsApi.Dtos;
using Xunit;

namespace ContactsApiTests
{
    public class ContactDataControllerIntegrationTest
    {
        private readonly HttpClient _httpClient;

        public ContactDataControllerIntegrationTest() =>
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44329/api/") };

        [Fact]
        public async Task GetContactDataForNonExistingContactAsync()
        {
            // Arrange
            var contactId = 0;
            var endpoint = $"contactdata?contactId={contactId}";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            string responseValue = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
            Assert.False(string.IsNullOrEmpty(responseValue));
        }

        [Fact]
        public async Task GetContactDataForExistingContactAsync()
        {
            // Arrange
            var endpoint = "contacts";
            var contact = (await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(endpoint)).FirstOrDefault();

            if (contact is null)
            {
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
                
                _ = await _httpClient.PostAsJsonAsync(endpoint, postContact);

                contact = (await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(endpoint)).FirstOrDefault();
            }

            var contactId = contact.Id;
            endpoint = $"contactdata?contactId={contactId}";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
