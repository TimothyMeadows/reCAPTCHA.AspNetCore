using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace reCAPTCHA.AspNetCore.Tests
{
    public class RecaptchaJsonSectionConfigurationTests
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _server;

        public RecaptchaJsonSectionConfigurationTests()
        {
            var builder = new WebHostBuilder().ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();
            }).UseStartup<Startup>();

            _server = new TestServer(builder);
            _httpClient = _server.CreateClient();
        }

        [Fact]
        public async Task Registering_Recaptcha_Via_Json_Section_Configuration_Should_Not_Throw_Exception()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("/home/index");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public void Registering_Recaptcha_Via_Json_Section_Configuration_Settings_Should_Not_Be_Null()
        {
            // Arrange

            // Act
            var settings = _server.Services.GetService<IOptions<RecaptchaSettings>>();

            // Assert
            Assert.NotNull(settings);
            Assert.NotNull(settings.Value.SecretKey);
            Assert.NotNull(settings.Value.SiteKey);
        }
    }
}