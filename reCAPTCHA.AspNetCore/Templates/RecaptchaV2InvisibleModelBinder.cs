using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class RecaptchaV2Invisible
    {
        public readonly Versions.RecaptchaV2Invisible Model;

        public RecaptchaV2Invisible(Versions.RecaptchaV2Invisible model)
        {
            Model = model;
        }
    }
}
