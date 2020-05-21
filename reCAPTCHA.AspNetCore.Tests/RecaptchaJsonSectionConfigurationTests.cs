using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace reCAPTCHA.AspNetCore.Tests
{
    public class RecaptchaJsonSectionConfigurationTests
    {
        private readonly HttpClient _httpClient;

        public RecaptchaJsonSectionConfigurationTests()
        {
            var builder = new WebHostBuilder().ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", false, true)
                    .AddEnvironmentVariables();
            }).UseStartup<Startup2>();

            //var builder2 = Host.CreateDefaultBuilder(new string[] { })
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>();
            //    }).Build();

            var testServer = new TestServer(builder);
            _httpClient = testServer.CreateClient();
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
    }
}