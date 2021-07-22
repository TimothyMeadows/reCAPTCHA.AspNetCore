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
        public static HtmlString Recaptcha<T>(this IHtmlHelper helper, RecaptchaSettings settings, T model = default)
        {
            if (settings == null)
                throw new ArgumentException("settings can't be null");

            var name = typeof(T).Name;
            var instance = (object)model ?? (name switch
            {
                nameof(RecaptchaV2Checkbox) => new RecaptchaV2Checkbox(),
                nameof(RecaptchaV2Invisible) => new RecaptchaV2Invisible(),
                nameof(RecaptchaV3HiddenInput) => new RecaptchaV3HiddenInput(),
                _ => throw new ArgumentException(
                    $"Unknown type '{name}' passed as recaptcha version. Please use a valid type for T when using the Recaptcha method.")
            });
            if (instance is RecaptchaVersion recaptchaVersion)
            {
                recaptchaVersion.Settings ??= settings;
            }
            var body = instance switch
            {
                RecaptchaV2Checkbox v2Checkbox => new Templates.RecaptchaV2Checkbox(v2Checkbox).TransformText(),
                RecaptchaV2Invisible v2Invisible => new Templates.RecaptchaV2Invisible(v2Invisible).TransformText(),
                RecaptchaV3HiddenInput v3 => new Templates.RecaptchaV3HiddenInput(v3).TransformText(),
                _ => string.Empty
            };

            return new HtmlString(body);
        }
    }
}
