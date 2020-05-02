using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace reCAPTCHA.AspNetCore.Attributes
{
    /// <summary>
    /// Validates Recaptcha submitted by a form using: @Html.Recaptcha(RecaptchaSettings.Value)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ValidateRecaptchaAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;
        private readonly double _minimumScore;


        /// <summary>
        /// Validates Recaptcha submitted by a form using: @Html.Recaptcha(RecaptchaSettings.Value)
        /// </summary>
        /// <param name="score">The minimum score you wish to be acceptable for a success.</param>
        public ValidateRecaptchaAttribute(double score = 0)
        {
            _minimumScore = score;
        }

        public IFilterMetadata CreateInstance(IServiceProvider services)
        {
            var recaptcha = services.GetService<IRecaptchaService>();
            return new ValidateRecaptchaFilter(recaptcha, _minimumScore);
        }
    }
}
