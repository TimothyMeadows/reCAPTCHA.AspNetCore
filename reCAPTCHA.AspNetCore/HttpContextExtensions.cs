using Microsoft.AspNetCore.Http;

namespace reCAPTCHA.AspNetCore
{
    public static class HttpContextExtensions
    {
        public static RecaptchaResponse GetCachedRecaptchaResponse(this HttpRequest request) =>
            request.HttpContext.Items["RecaptchaResponse"] as RecaptchaResponse;
    }
}
