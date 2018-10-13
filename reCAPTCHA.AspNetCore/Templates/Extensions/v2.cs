using System;
using System.Collections.Generic;
using System.Text;
using reCAPTCHA.AspNetCore.Models;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class v2
    {
        private v2Model Model { get; }

        public v2(v2Model model)
        {
            Model = model;
        }
    }
}
