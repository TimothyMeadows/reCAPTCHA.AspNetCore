using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Models
{
    public class v3Model
    {
        public Guid Uid { get; set; }
        public string Action { get; set; }
        public string Language { get; set; }
        public RecaptchaSettings Settings { get; set; }
    }
}
