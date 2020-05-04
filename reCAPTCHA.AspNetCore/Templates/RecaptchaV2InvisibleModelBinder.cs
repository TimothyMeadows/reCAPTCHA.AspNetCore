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
            if (model.Settings == null)
                throw new ArgumentException("Settings can't be null.");

            var defaultModel = new Versions.RecaptchaV2Invisible();
            if (model.Uid.Equals(Guid.Empty))
                model.Uid = defaultModel.Uid;

            if (string.IsNullOrEmpty(model.Language))
                model.Language = defaultModel.Language;

            if (string.IsNullOrEmpty(model.Id))
                model.Id = defaultModel.Id;

            Model = model;
        }
    }
}
