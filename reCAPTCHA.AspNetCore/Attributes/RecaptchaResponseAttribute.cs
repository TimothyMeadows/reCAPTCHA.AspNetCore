using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace reCAPTCHA.AspNetCore.Attributes
{
    /// <summary>
    /// Validates reCaptcha response code bound to a property, parameter or field.
    /// </summary>
    /// <seealso cref="ValidationAttribute" />
    /// <seealso cref="IRecaptchaService.Validate(string)"/>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field)]
	public class RecaptchaResponseAttribute : ValidationAttribute
	{
		/// <summary>
		/// Gets a value that indicates whether the attribute requires validation context.
		/// </summary>
		public override bool RequiresValidationContext => true;

        /// <summary>
        /// Gets or sets the minimum score.
        /// </summary>
        /// <value>
        /// The minimum score.
        /// </value>
        /// <seealso cref="ValidateRecaptchaAttribute._minimumScore"/>
        public double MinimumScore { get; set; }

        /// <summary>
        /// Returns wether the captcha validation was deemed successful based on the value of a member to attribute has been applied to.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        /// <exception cref="ConfigurationErrorsException">When reCaptcha has not been configured.</exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!(value is string responseCode)
					|| string.IsNullOrWhiteSpace(responseCode))
				return null;

			var validationservice = validationContext.GetService<IRecaptchaService>();

            if(validationservice is null)
                throw new ConfigurationErrorsException($"{typeof(IRecaptchaService).Assembly.GetName().Name} has not been configured");

			var response = validationservice.Validate(responseCode).Result;

            if (response.success 
                && (response.score == 0 || response.score >= MinimumScore))

                return ValidationResult.Success;

            else

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
	}
}
