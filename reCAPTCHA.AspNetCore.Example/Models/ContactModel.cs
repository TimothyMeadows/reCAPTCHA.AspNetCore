using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reCAPTCHA.AspNetCore.Example.Models
{
    public class ContactModel
    {
		[Required(ErrorMessage = "A Message is required.")]
        public string Message { get; set; }
		public RecaptchaSettings CaptchaSettings { get; set; }

		public ContactModel()
		{
		}
    }
}
