using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore.Attributes;
using reCAPTCHA.AspNetCore.Example.Models;

namespace reCAPTCHA.AspNetCore.Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecaptchaService _recaptcha;
        private readonly double _minimumScore;
        private readonly string _errorMessage;

        public HomeController(IRecaptchaService recaptcha)
        {
            _recaptcha = recaptcha;
            _minimumScore = 0.5;
            _errorMessage = "There was an error validating the google recaptcha response. Please try again, or contact the site owner.";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View(new ContactModel());
        }

        public IActionResult Contact2()
        {
            ViewData["Message"] = "Your contact page.";

            return View(new ContactModel());
        }

        public IActionResult Contact3()
        {
            ViewData["Message"] = "Your contact page.";

            return View(new ContactModel());
        }

        [HttpPost]
        [ValidateRecaptcha(0.5)]
        public IActionResult Contact(ContactModel model)
        {
            ViewData["Message"] = "Your contact page.";

            return View(!ModelState.IsValid ? model : new ContactModel());
        }

        [HttpPost]
        public async Task<IActionResult> Contact2(ContactModel model)
        {
            ViewData["Message"] = "Your contact page.";

            var recaptcha = await _recaptcha.Validate(Request);
            if (!recaptcha.success || recaptcha.score != 0 && recaptcha.score < _minimumScore)
                ModelState.AddModelError("Recaptcha", _errorMessage);

            return View(!ModelState.IsValid ? model : new ContactModel());
        }

        [HttpPost]
        public async Task<IActionResult> Contact3(ContactModel model)
        {
            ViewData["Message"] = "Your contact page.";

            return View(!ModelState.IsValid ? model : new ContactModel());
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
