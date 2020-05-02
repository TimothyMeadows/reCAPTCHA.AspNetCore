using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reCAPTCHA.AspNetCore.Attributes;
using reCAPTCHA.AspNetCore.Example.Models;

namespace reCAPTCHA.AspNetCore.Example.Controllers
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


        public IActionResult Contact()
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
