using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
    }
}
