using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using ContactsApi.Dtos;
using ContactsApiTests.Fixtures;
using Xunit;

namespace ContactsApiTests
{
    [Collection("Automapper collection")]
    public class ContactsControllerIntegrationTest
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ContactsControllerIntegrationTest(AutoMapperFixture autoMapperFixture)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44329/api/") };
            _mapper = autoMapperFixture.Mapper;
        }

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

            var contacts = await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(endpoint);
            ContactDto contactDto = _mapper.Map<ContactDto>(postContact);

            if (contacts.Contains(contactDto))
            {
                var deleteContact = _mapper.Map<DeleteContactDto>(contactDto);

                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = new StringContent(JsonSerializer.Serialize(deleteContact), Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{_httpClient.BaseAddress}{endpoint}")
                };
            }

            // Act
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, postContact);
            ContactDto contact = await response.Content.ReadFromJsonAsync<ContactDto>();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.True(contact.Id > 0);
        }

        [Fact]
        public async Task DeleteExistingContactAsync()
        {
            // Arrange
            var endpoint = "contacts";
            var contact = (await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(endpoint)).FirstOrDefault();

            if (contact is null)
            {
                await PostNonExistingContactAsync();
                contact = (await _httpClient.GetFromJsonAsync<IEnumerable<ContactDto>>(endpoint)).FirstOrDefault();
            }

            var deleteContact = _mapper.Map<DeleteContactDto>(contact);

            // Act
            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(deleteContact), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_httpClient.BaseAddress}{endpoint}")
            };
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NoContent);
        }
    }
}
