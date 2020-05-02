using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Versions
{
    public class RecaptchaV2Invisible : RecaptchaVersion
    {
        public string Id { get; set; } = "recaptcha";
        public string SuccessCallback { get; set; }
        public string ErrorCallback { get; set; }
        public string ExpiredCallback { get; set; }
    }
}
