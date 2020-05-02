using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using reCAPTCHA.AspNetCore.Versions;

namespace reCAPTCHA.AspNetCore
{
    public static class RecaptchaHelper
    {
        /// <summary>
        /// Helper extension to render the Google Recaptcha.
        /// </summary>
        /// <param name="helper">Html helper object.</param>
        /// <param name="settings">Recaptcha settings needed to render.</param>
        /// <param name="model">Optional recaptcha version model. If not supplied a model with defaults will be created.</param>
        /// <returns>HtmlString with Recaptcha elements.</returns>
        public static HtmlString Recaptcha<T>(this IHtmlHelper helper, RecaptchaSettings settings, T model = default(T))
        {
            if (settings == null)
                throw new ArgumentException("settings can't be null");

            if (model == null)
                throw new ArgumentException("model can't be null");

            string body;
            var instance = Convert.ChangeType(model, typeof(T));
            var name = nameof(T);
            switch (name)
            {
                case nameof(RecaptchaV2Checkbox):
                    var v2Checkbox = (RecaptchaV2Checkbox)instance;
                    body = new Templates.RecaptchaV2Checkbox(v2Checkbox).TransformText();
                    break;
                case nameof(RecaptchaV2Invisible):
                    var v2Invisible = (RecaptchaV2Invisible)instance;
                    body = new Templates.RecaptchaV2Invisible(v2Invisible).TransformText();
                    break;
                case nameof(RecaptchaV3HiddenInput):
                    var v3 = (RecaptchaV3HiddenInput)instance;
                    body = new Templates.RecaptchaV3HiddenInput(v3).TransformText();
                    break;
                default:
                    throw new ArgumentException($"Unknown type '{name}' passed as recaptcha version. Please use a valid type for T when using the Recaptcha method.");
            }

            return new HtmlString(body);
        }
    }
}
