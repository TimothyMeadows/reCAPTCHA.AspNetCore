using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaSettings
    {
        public string SecretKey { get; set; }
        public string SiteKey { get; set; }
    }
}