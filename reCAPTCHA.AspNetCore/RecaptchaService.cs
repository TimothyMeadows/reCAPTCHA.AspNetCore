using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly RecaptchaSettings recaptchaSettings;

        public RecaptchaService(RecaptchaSettings settings)
        {
            recaptchaSettings = settings;
            Client = new HttpClient();
        }

        public RecaptchaService(RecaptchaSettings settings, HttpClient client)
        {
            recaptchaSettings = settings;
            Client = client;
        }

        public static HttpClient Client { get; private set; }

        public async Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response")) // error if no reason to do anything, this is to alert developers they are calling it without reason.
                throw new ValidationException("Google recaptcha response not found in form. Did you forget to include it?");

            var response = request.Form["g-recaptcha-response"];
            var result = await Client.GetStringAsync($"https://{recaptchaSettings.Site}/recaptcha/api/siteverify?secret={recaptchaSettings.SecretKey}&response={response}");
            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(result);

            if (captchaResponse.success && antiForgery)
                if (captchaResponse.hostname?.ToLower() != request.Host.Host?.ToLower())
                    throw new ValidationException("Recaptcha host, and request host do not match. Forgery attempt?");

            return captchaResponse;
        }

        public async Task<RecaptchaResponse> Validate(string responseCode)
        {
            if (string.IsNullOrEmpty(responseCode))
                throw new ValidationException("Google recaptcha response is empty?");

            var result = await Client.GetStringAsync($"https://{recaptchaSettings.Site}/recaptcha/api/siteverify?secret={recaptchaSettings.SecretKey}&response={responseCode}");
            var captchaResponse = JsonSerializer.Deserialize<RecaptchaResponse>(result);

            return captchaResponse;
        }
    }
}