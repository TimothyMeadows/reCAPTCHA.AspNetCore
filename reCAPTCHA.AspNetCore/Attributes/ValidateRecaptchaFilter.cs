using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace reCAPTCHA.AspNetCore.Attributes
{
    public class ValidateRecaptchaFilter : IAsyncActionFilter
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly double _minimumScore;
        private readonly string _errorMessage;

        public ValidateRecaptchaFilter(IRecaptchaService recaptcha, double minimumScore, string errorMessage)
        {
            _recaptcha = recaptcha;
            _minimumScore = minimumScore;
            _errorMessage = errorMessage;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var recaptcha = await _recaptcha.Validate(context.HttpContext.Request);
            if (!recaptcha.success || recaptcha.score != 0 && recaptcha.score < _minimumScore)
                context.ModelState.AddModelError("Recaptcha", _errorMessage);

            await next();
        }
    }
}
