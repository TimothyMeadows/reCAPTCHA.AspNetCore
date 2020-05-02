using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace reCAPTCHA.AspNetCore.Attributes
{
    public class ValidateRecaptchaFilter : IAsyncActionFilter
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly double _minimumScore;

        public ValidateRecaptchaFilter(IRecaptchaService recaptcha, double minimumScore)
        {
            _recaptcha = recaptcha;
            _minimumScore = minimumScore;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var recaptcha = await _recaptcha.Validate(context.HttpContext.Request);
            if (!recaptcha.success || recaptcha.score != 0 && recaptcha.score < _minimumScore)
                context.ModelState.AddModelError("Recaptcha", "There was an error validating the google recaptcha response. Please try again, or contact the site owner.");

            await next();
        }
    }
}
