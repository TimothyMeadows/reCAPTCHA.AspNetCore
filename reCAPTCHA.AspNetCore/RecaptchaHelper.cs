using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reCAPTCHA.AspNetCore
{
    public static class RecaptchaHelper
    {
        public static HtmlString Recaptcha(this IHtmlHelper helper, string siteKey)
        {
            var uid = Guid.NewGuid().ToString();
            return new HtmlString($"<div id=\"{uid}\" class=\"g-recaptcha\" data-sitekey=\"{siteKey}\"></div>\r\n" +
                                  "<script>\r\n" +
                                  $"if (typeof grecaptcha !== 'undefined') {{\r\ngrecaptcha.render(\'{uid}\', {{\r\n \'sitekey\' : \'{siteKey}\'\r\n }});\r\n}}\r\n" +
                                  "</script>");
        }
    }
}
