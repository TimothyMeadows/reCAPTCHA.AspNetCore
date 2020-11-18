using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore.Attributes;
using reCAPTCHA.AspNetCore.Example.Models;
using System.ComponentModel.DataAnnotations;

namespace reCAPTCHA.AspNetCore.Example.Controllers
{
    [ApiController]
    public class SomeApiController : Controller
    {
        [HttpGet]
        public ActionResult SomeAction(string someValue, [Required, RecaptchaResponse] string reCaptchaResponseCode)
        {
            return Ok($"I have done something with {someValue} after reCaptcha was validated");
        }

        [HttpPost]
        public ActionResult SomeAction(OtherModel model)
        {
            return Ok($"I have done something after reCaptcha was validated");
        }
    }
}
