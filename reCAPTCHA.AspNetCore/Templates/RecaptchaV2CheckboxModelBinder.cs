using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class RecaptchaV2Checkbox
    {
        public readonly Versions.RecaptchaV2Checkbox Model;

        public RecaptchaV2Checkbox(Versions.RecaptchaV2Checkbox model)
        {
            Model = model;
        }
    }
}
