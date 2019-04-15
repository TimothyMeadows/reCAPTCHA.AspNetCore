using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaService : IRecaptchaService
    {
        public static bool UseRecaptchaNet { get; set; } = false;
        public static HttpClient Client { get; private set; }
        public readonly RecaptchaSettings RecaptchaSettings;

        public RecaptchaService(RecaptchaSettings options)
        {
            RecaptchaSettings = options;

            if (Client == null)
                Client = new HttpClient();
        }

        public RecaptchaService(IOptions<RecaptchaSettings> options)
        {
            RecaptchaSettings = options.Value;

            if(Client == null)
                Client = new HttpClient();
        }

        public RecaptchaService(IOptions<RecaptchaSettings> options, HttpClient client)
        {
            RecaptchaSettings = options.Value;
            Client = client;
        }

        public RecaptchaService(RecaptchaSettings options, HttpClient client)
        {
            RecaptchaSettings = options;
            Client = client;
        }

        public async Task<RecaptchaResponse> Validate(HttpRequest request, bool antiForgery = true)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response")) // error if no reason to do anything, this is to alert developers they are calling it without reason.
                throw new ValidationException("Google recaptcha response not found in form. Did you forget to include it?");

            var domain = UseRecaptchaNet ? "www.recaptcha.net" : "www.google.com";
            var response = request.Form["g-recaptcha-response"];
            var result = await Client.GetStringAsync($"https://{domain}/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={response}");
            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

            if (captchaResponse.success && antiForgery)
                if (captchaResponse.hostname?.ToLower() != request.Host.Host?.ToLower())
                    throw new ValidationException("Recaptcha host, and request host do not match. Forgery attempt?");

            return captchaResponse;
        }

        public async Task<RecaptchaResponse> Validate(string responseCode)
        {
            if (string.IsNullOrEmpty(responseCode))
                throw new ValidationException("Google recaptcha response is empty?");

            var domain = UseRecaptchaNet ? "www.recaptcha.net" : "www.google.com";
            var result = await Client.GetStringAsync($"https://{domain}/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={responseCode}");
            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);
            return captchaResponse;
        }
    }
}
