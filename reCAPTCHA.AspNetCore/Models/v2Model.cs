using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Models
{
    public class v2Model
    {
        public string Id { get; set; }
        public Guid Uid { get; set; }
        public string Method { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
        public RecaptchaSettings Settings { get; set; }
        public string SuccessCallback { get; set; }
        public string ErrorCallback { get; set; }
        public string ExpiredCallback { get; set; }
    }
}
