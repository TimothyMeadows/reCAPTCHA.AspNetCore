using System;
using System.Collections.Generic;
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
        public readonly RecaptchaSettings RecaptchaSettings;

        public RecaptchaService(IOptions<RecaptchaSettings> options)
        {
            RecaptchaSettings = options.Value;
        }

        public async Task<bool> Validate(HttpRequest request)
        {
            if (!request.Form.ContainsKey("g-recaptcha-response"))
                return false;

            var response = request.Form["g-recaptcha-response"];
            var client = new HttpClient();
            var result = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={RecaptchaSettings.SecretKey}&response={response}");
            var captchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(result);

            // TODO: Maybe parse Request.Host and RecaptchaResponse.host and see if they match?
            return captchaResponse.success;
        }
    }
}
