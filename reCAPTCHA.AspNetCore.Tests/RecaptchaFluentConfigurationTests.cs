using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace reCAPTCHA.AspNetCore.Tests
{
    public class RecaptchaFluentConfigurationTests
    {
        private readonly HttpClient _httpClient;

        public RecaptchaFluentConfigurationTests()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            _httpClient = testServer.CreateClient();
        }

        [Fact]
        public async Task Registering_Recaptcha_Via_Fluent_Configuration_Should_Not_Throw_Exception()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/home/index");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}