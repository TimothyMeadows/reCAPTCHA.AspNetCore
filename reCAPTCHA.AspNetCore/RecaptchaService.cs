using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaService : IRecaptchaService
    {
        private static readonly HttpClient Client = new HttpClient();
        public readonly RecaptchaSettings RecaptchaSettings;

        public RecaptchaService(IOptions<RecaptchaSettings> options)
        {
            RecaptchaSettings = options.Value;
        }

        public async Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response")) // error if no reason to do anything, this is to alert developers they are calling it without reason.
                throw new ValidationException("Google recaptcha response not found in form. Did you forget to include it?");

            if (request.GetCachedRecaptchaResponse() == null)
                request.HttpContext.Items["RecaptchaResponse"] = await Validate(request.Form["g-recaptcha-response"]);
            RecaptchaResponse captchaResponse = request.GetCachedRecaptchaResponse();

            if (captchaResponse.success && antiForgery)
                if (captchaResponse.hostname?.ToLower() != request.Host.Host?.ToLower() && captchaResponse.hostname != "testkey.google.com")
                    throw new ValidationException("Recaptcha host, and request host do not match. Forgery attempt?");

            return captchaResponse;
        }

        public async Task<RecaptchaResponse> Validate(string responseCode)
        {
            if (string.IsNullOrEmpty(responseCode))
                throw new ValidationException("Google recaptcha response is empty?");

            var result = await Client.GetStringAsync($"https://{RecaptchaSettings.Site}/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={responseCode}");
            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(result);

            return captchaResponse;
        }
    }
}