using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reCAPTCHA.AspNetCore
{
    public static class RecaptchaHelper
    {
        public static HtmlString Recaptcha(this IHtmlHelper helper, string siteKey)
        {
            return new HtmlString($"<div class=\"g-recaptcha\" data-sitekey=\"{siteKey}\"></div>");
        }
    }
}
