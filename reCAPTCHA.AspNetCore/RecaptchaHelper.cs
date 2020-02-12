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
        /// <param name="language">Google Recaptcha <a href="https://developers.google.com/recaptcha/docs/language">Language Code</a></param>
        /// <param name="id">Google Recaptcha v2-invis button id. This id can't be named submit due to a naming bug.</param>
        /// <param name="successCallback">Google Recaptcha v2/v2-invis success callback method.</param>
        /// <param name="errorCallback">Google Recaptcha v2/v2-invis error callback method.</param>
        /// <param name="expiredCallback">Google Recaptcha v2/v2-invis expired callback method.</param>
        /// <returns>HtmlString with Recaptcha elements</returns>
        public static HtmlString Recaptcha(this IHtmlHelper helper, RecaptchaSettings settings, string theme = "light", string action = "homepage", string language = "en", string id = "recaptcha", string successCallback = null, string errorCallback = null, string expiredCallback = null)
        {
            if (!settings.Enabled) return new HtmlString("<!-- Google Recaptcha disabled -->");

            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("id can't be null");

            if (id.ToLower() == "submit")
                throw new ArgumentException("id can't be named submit");

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
                        Theme = theme,
                        Language = language,
                        SuccessCallback = successCallback,
                        ErrorCallback = errorCallback,
                        ExpiredCallback = expiredCallback
                    }).TransformText());
                case "v2-invis":
                    return new HtmlString(new v2Invis(new v2Model()
                    {
                        Settings = settings,
                        Id = id,
                        Uid = uid,
                        Method = method,
                        Theme = theme,
                        Language = language,
                        SuccessCallback = successCallback,
                        ErrorCallback = errorCallback,
                        ExpiredCallback = expiredCallback
                    }).TransformText());
                case "v3":
                    return new HtmlString(new v3(new v3Model()
                    {
                        Settings = settings,
                        Uid = uid,
                        Action = action,
                        Language = language
                    }).TransformText());
            }
        }
    }
}
