using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Versions
{
    /// <summary>
    /// This is the base model for Google Recaptcha version models. You should only add new properties here if their global across all recaptcha versions.
    /// </summary>
    public class RecaptchaVersion
    {
        public Guid Uid { get; set; } = Guid.NewGuid();
        public string Language { get; set; } = "en";
        public RecaptchaSettings Settings { get; set; }
    }
}
