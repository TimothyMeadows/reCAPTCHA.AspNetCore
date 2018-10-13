using System;
using System.Numerics;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using reCAPTCHA.AspNetCore.Models;
using reCAPTCHA.AspNetCore.Templates;

namespace reCAPTCHA.AspNetCore
{
    public static class RecaptchaHelper
    {
        /// <summary>
        /// Helper extension to render the Google Recaptcha.
        /// </summary>
        /// <param name="helper">Html helper object.</param>
        /// <param name="settings">Recaptcha settings needed to render.</param>
        /// <param name="theme">Google Recaptcha theme default is light</param>
        /// <param name="action">Google Recaptcha v3 <a href="https://developers.google.com/recaptcha/docs/v3#actions">Action</a></param>
        /// <returns>HtmlString with Recaptcha elements</returns>
        public static HtmlString Recaptcha(this IHtmlHelper helper, RecaptchaSettings settings, string theme = "light", string action = "homepage")
        {
            var uid = Guid.NewGuid();
            var method = uid.ToString().Replace("-", "_");

            switch (settings.Version)
            {
                default:
                case "v2":
                    return new HtmlString(new v2(new v2Model()
                    {
                        Settings = settings,
                        Uid = uid,
                        Method = method,
                        Theme = theme
                    }).TransformText());
                case "v2-invis":
                    return new HtmlString(new v2Invis(new v2Model()
                    {
                        Settings = settings,
                        Uid = uid,
                        Method = method,
                        Theme = theme
                    }).TransformText());
                case "v3":
                    return new HtmlString(new v3(new v3Model()
                    {
                        Settings = settings,
                        Uid = uid,
                        Action = action
                    }).TransformText());
            }
        }
    }
}
