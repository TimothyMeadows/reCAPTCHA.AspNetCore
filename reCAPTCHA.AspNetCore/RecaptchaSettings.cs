using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore
{
	public interface IRecaptchaSettings
	{
		string SecretKey { get; set; }
		string SiteKey { get; set; }
		string Version { get; set; }
	}

    public class RecaptchaSettings: IRecaptchaSettings
    {
	    /// <summary>
	    /// Google Recaptcha Secret Key
	    /// </summary>
        public string SecretKey { get; set; }
	    /// <summary>
	    /// Google Recaptcha Site Key
	    /// </summary>
        public string SiteKey { get; set; }
	    /// <summary>
	    /// Google Recaptcha Version
	    /// </summary>
        public string Version { get; set; }
    }
}