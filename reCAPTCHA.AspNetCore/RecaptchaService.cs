using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly ILogger<RecaptchaService> _logger;
        private static readonly HttpClient Client = new HttpClient();
        public readonly RecaptchaSettings RecaptchaSettings;

        public RecaptchaService(IOptions<RecaptchaSettings> options, ILogger<RecaptchaService> logger)
        {
            _logger = logger;
            RecaptchaSettings = options.Value;
        }

        public async Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response")) // error if no reason to do anything, this is to alert developers they are calling it without reason.
            {
                _logger.LogError("Google recaptcha response not found in form. Did you forget to include it?");
                return new RecaptchaResponse(false);
            }

            var response = request.Form["g-recaptcha-response"];
            var result = await Client.GetStringAsync($"https://{RecaptchaSettings.Site}/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={response}");
            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(result);

            if (captchaResponse.success && antiForgery)
                if (captchaResponse.hostname?.ToLower() != request.Host.Host?.ToLower() &&
                    captchaResponse.hostname != "testkey.google.com")
                {
                    _logger.LogError("Recaptcha host, and request host do not match. Forgery attempt?");
                    return new RecaptchaResponse(false);
                }

            return captchaResponse;
        }

        public async Task<RecaptchaResponse> Validate(string responseCode)
        {
            if (string.IsNullOrEmpty(responseCode))
            {
                _logger.LogError("Google recaptcha response is empty?");
                return new RecaptchaResponse(false);
            }

            var result = await Client.GetStringAsync($"https://{RecaptchaSettings.Site}/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={responseCode}");
            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(result);

            return captchaResponse;
        }
    }
}