using Microsoft.AspNetCore.Mvc;

namespace reCAPTCHA.AspNetCore.Tests.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IRecaptchaService recaptcha)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}