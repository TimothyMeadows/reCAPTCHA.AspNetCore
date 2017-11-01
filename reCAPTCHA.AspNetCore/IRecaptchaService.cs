using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace reCAPTCHA.AspNetCore
{
    public interface IRecaptchaService
    {
        Task<bool> Validate(HttpRequest request);
    }
}
