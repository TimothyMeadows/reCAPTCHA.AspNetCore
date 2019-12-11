using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace reCAPTCHA.AspNetCore
{
    /// <summary>
    /// Validates Recaptcha submitted by a form using: @Html.Recaptcha(RecaptchaSettings.Value)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ValidateRecaptchaAttribute : Attribute, IFilterFactory
    {
        private readonly string _recaptchaModelErrorMessage;
        private readonly bool _antiForgery;

        public bool IsReusable => true;

        public ValidateRecaptchaAttribute(string recaptchaModelErrorMessage = "Your request cannot be completed because you failed Recaptcha verification.", bool antiForgery = true)
        {
            _recaptchaModelErrorMessage = recaptchaModelErrorMessage;
            _antiForgery = antiForgery;
        }

        public IFilterMetadata CreateInstance(IServiceProvider services)
        {
            var filter = services.GetService<ValidateRecaptchaFilter>();
            filter.RecaptchaModelErrorMessage = _recaptchaModelErrorMessage;
            filter.AntiForgery = _antiForgery;
            return filter;
        }
    }

    public class ValidateRecaptchaFilter : IAsyncActionFilter
    {
        private readonly IRecaptchaService _recaptcha;

        public string RecaptchaModelErrorMessage { private get; set; }
        public bool AntiForgery { private get; set; }

        public ValidateRecaptchaFilter(IRecaptchaService recaptcha)
        {
            _recaptcha = recaptcha;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var recaptcha = await _recaptcha.Validate(context.HttpContext.Request, AntiForgery).ConfigureAwait(true);
            if (!recaptcha.success) context.ModelState.AddModelError("", RecaptchaModelErrorMessage);
        }
    }
}
