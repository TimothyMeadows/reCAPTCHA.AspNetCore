namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaSettings
    {
        /// <summary>
        /// Google Recaptcha Site this is often www.google.com, or www.recaptcha.net. You can also use a custom proxy address.
        /// </summary>
        public string Site { get; set; } = "www.google.com";
        /// <summary>
        /// Google Recaptcha Secret Key
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// Google Recaptcha Site Key
        /// </summary>
        public string SiteKey { get; set; }
    }
}