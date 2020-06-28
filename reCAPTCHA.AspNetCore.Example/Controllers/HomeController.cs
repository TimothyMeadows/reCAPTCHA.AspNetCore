using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore.Attributes;
using reCAPTCHA.AspNetCore.Example.Models;

namespace reCAPTCHA.AspNetCore.Example.Controllers
{
    public class HomeController : Controller
    {
		public IRecaptchaService CaptchaSvc { get; set; }
		public RecaptchaSettings CaptchSettings { get; set; }

        public HomeController(IRecaptchaService recaptcha)
        {
			CaptchSettings = recaptcha.RecaptchaSettings;
			CaptchaSvc = recaptcha;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

			return View(new ContactModel { CaptchaSettings = CaptchSettings });
        }

        [HttpPost]
		[ValidateRecaptcha(0.5, errorMessage: "reCaptcha Error")]
        public IActionResult Contact(ContactModel model)
        {
            ViewData["Message"] = "Your contact page.";
			model.CaptchaSettings = CaptchSettings;

			if (!ModelState.IsValid)
			{
				return View(new ContactModel { CaptchaSettings = CaptchSettings });

			}
			return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
