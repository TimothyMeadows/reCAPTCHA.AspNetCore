using reCAPTCHA.AspNetCore.Attributes;

namespace reCAPTCHA.AspNetCore.Example.Models
{
    public class OtherModel
    {
        [RecaptchaResponse(MinimumScore = 0.5)]
        public string ReCaptchaResponseCode { get; set; }
    }
}
