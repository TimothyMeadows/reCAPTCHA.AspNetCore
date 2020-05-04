namespace reCAPTCHA.AspNetCore
{
    public class RecaptchaSettings
    {
        /// <summary>
        /// Google Recaptcha Site this is often www.google.com, or www.recaptcha.net. You can also use a custom proxy address.
        /// </summary>
        public string Site { get; set; } = "www.google.com";
        /// <summary>
        /// Values for the content security policy directives. See: https://developers.google.com/recaptcha/docs/faq#im-using-content-security-policy-csp-on-my-website.-how-can-i-configure-it-to-work-with-recaptcha
        /// </summary>
        public string ContentSecurityPolicy { get; set; } = string.Empty;
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