using System;
using System.Collections.Generic;
using System.Text;
using reCAPTCHA.AspNetCore.Models;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class v3
    {
        private v3Model Model { get; }

        public v3(v3Model model)
        {
            Model = model;
        }
    }
}
