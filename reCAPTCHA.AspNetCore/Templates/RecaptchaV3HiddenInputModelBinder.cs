using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class RecaptchaV3HiddenInput
    {
        public readonly Versions.RecaptchaV3HiddenInput Model;

        public RecaptchaV3HiddenInput(Versions.RecaptchaV3HiddenInput model)
        {
            Model = model;
        }
    }
}
