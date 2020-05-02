namespace reCAPTCHA.AspNetCore.Versions
{
    public class RecaptchaV2Checkbox : RecaptchaVersion
    {
        public string Id { get; set; } = "recaptcha";
        public string Theme { get; set; } = "light";
        public string SuccessCallback { get; set; }
        public string ErrorCallback { get; set; }
        public string ExpiredCallback { get; set; }
    }
}
