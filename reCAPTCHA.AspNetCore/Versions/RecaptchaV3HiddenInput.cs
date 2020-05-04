using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Versions
{
    public class RecaptchaV3HiddenInput : RecaptchaVersion
    {
        public string Action { get; set; } = "homepage";
    }
}
