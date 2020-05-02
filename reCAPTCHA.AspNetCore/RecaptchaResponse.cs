using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
        public string action { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
    }
}
