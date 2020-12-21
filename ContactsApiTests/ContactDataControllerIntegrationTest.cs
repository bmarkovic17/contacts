using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            var contactId = 23;
            var endpoint = $"contactdata?contactId={contactId}";

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
