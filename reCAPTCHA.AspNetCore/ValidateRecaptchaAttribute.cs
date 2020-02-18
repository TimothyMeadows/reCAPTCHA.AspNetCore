using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace reCAPTCHA.AspNetCore
{
    /// <summary>
    /// Validates Recaptcha submitted by a form using: @Html.Recaptcha(RecaptchaSettings.Value)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ValidateRecaptchaAttribute : Attribute, IFilterFactory
    {
        private readonly string _action;
        private readonly string _modelErrorMessage;
        private readonly decimal _minimumScore;

        public bool IsReusable => true;
        
        public ValidateRecaptchaAttribute(string action = "homepage", string modelErrorMessage = "Your request cannot be completed because you failed Recaptcha verification.", double minimumScore = 0.5)
        {
            _action = action;
            _modelErrorMessage = modelErrorMessage;
            _minimumScore = (decimal) minimumScore;
        }

        public IFilterMetadata CreateInstance(IServiceProvider services)
        {
            var recaptchaEnabled = services.GetService<RecaptchaSettings>().Enabled;
            var recaptchaService = services.GetService<IRecaptchaService>();
            return new ValidateRecaptchaFilter(recaptchaService, _action, _modelErrorMessage, _minimumScore, recaptchaEnabled);
        }
    }

    public class ValidateRecaptchaFilter : IAsyncActionFilter
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly string _action;
        private readonly string _modelErrorMessage;
        private readonly decimal _minimumScore;
        private readonly bool _enabled;

        public ValidateRecaptchaFilter(IRecaptchaService recaptcha, string action, string modelErrorMessage, decimal minimumScore, bool enabled)
        {
            _recaptcha = recaptcha;
            _action = action;
            _modelErrorMessage = modelErrorMessage;
            _minimumScore = minimumScore;
            _enabled = enabled;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           if (_enabled)
            {
                var recaptcha = await _recaptcha.Validate(context.HttpContext.Request);
                if (!recaptcha.success || recaptcha.action != _action || recaptcha.score < _minimumScore)
                    context.ModelState.AddModelError("Recaptcha", _modelErrorMessage);
            }
            next();
        }
    }
}
