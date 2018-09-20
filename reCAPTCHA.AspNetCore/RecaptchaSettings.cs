using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaSettings
    {
        /// <summary>
        /// Google Recaptcha Secret Key
        /// </summary>
        public string RecaptchaSecretKey { get; set; }
        /// <summary>
        /// Google Recaptcha Site Key
        /// </summary>
        public string RecaptchaSiteKey { get; set; }
        /// <summary>
        /// Google Recaptcha Version
        /// </summary>
        public string RecaptchaVersion { get; set; }
    }
}