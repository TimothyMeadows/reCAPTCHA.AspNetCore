using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace reCAPTCHA.AspNetCore
{
    public static class RecaptchaHelper
    {
        /// <summary>
        /// Helper extension to render the Google Recaptcha.
        /// </summary>
        /// <param name="helper">Html helper object.</param>
        /// <param name="settings">Recaptcha settings needed to render.</param>
        /// <param name="action">Google Recaptcha theme default is light</param>
        /// <param name="action">Google Recaptcha v3 <a href="https://developers.google.com/recaptcha/docs/v3#actions">Action</a></param>
        /// <returns>HtmlString with Recaptcha elements</returns>
        public static HtmlString Recaptcha(this IHtmlHelper helper, RecaptchaSettings settings, string theme = "light", string action = "homepage")
        {
            var uid = Guid.NewGuid().ToString();
            var method = uid.Replace("-", "_");

            switch (settings.RecaptchaVersion)
            {
                default:
                case "v2":
                    return new HtmlString(
                        $"<div id=\"{uid}\" class=\"g-recaptcha\" data-sitekey=\"{settings.RecaptchaSiteKey}\"></div>\r\n" +
                        "<script>\r\n" +
                        $"function _{method}() {{\r\n if (typeof grecaptcha !== 'undefined') {{\r\ngrecaptcha.render(\'{uid}\', {{\'sitekey\' : \'{settings.RecaptchaSiteKey}\', 'theme\' : \'{theme}\' }});}}\r\n}}\r\n" +
                        "</script>" +
                        $"<script src=\"https://www.google.com/recaptcha/api.js?onload=_{method}&render=explicit\" async defer></script>\r\n");
                case "v3":
                    return new HtmlString(
                        $"<input id=\"g-recaptcha-response\" name=\"g-recaptcha-response\" type=\"hidden\" value=\"\" />\r\n" +
                        $"<script src=\"https://www.google.com/recaptcha/api.js?render={settings.RecaptchaSiteKey}\"></script>\r\n" +
                        "<script>\r\n" +
                        $"if (typeof grecaptcha !== \'undefined\') {{\r\n grecaptcha.ready(function () {{\r\n grecaptcha.execute(\'{settings.RecaptchaSiteKey}\', {{ \'action\': \'{action}\' }}).then(function (token) {{\r\n document.getElementById(\'g-recaptcha-response\').value = token;\r\n }})\r\n }})\r\n}}" +
                        "</script>");
            }
        }
    }
}
