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
        private readonly string _errorMessage;


        /// <summary>
        /// Validates Recaptcha submitted by a form using: @Html.Recaptcha(RecaptchaSettings.Value)
        /// </summary>
        /// <param name="score">The minimum score you wish to be acceptable for a success.</param>
        /// <param name="errorMessage">Error message you want added to validation model.</param>
        public ValidateRecaptchaAttribute(double score = 0, string errorMessage = "There was an error validating the google recaptcha response. Please try again, or contact the site owner.")
        {
            _minimumScore = score;
            _errorMessage = errorMessage;
        }

        public IFilterMetadata CreateInstance(IServiceProvider services)
        {
            var recaptcha = services.GetService<IRecaptchaService>();
            return new ValidateRecaptchaFilter(recaptcha, _minimumScore, _errorMessage);
        }
    }
}
