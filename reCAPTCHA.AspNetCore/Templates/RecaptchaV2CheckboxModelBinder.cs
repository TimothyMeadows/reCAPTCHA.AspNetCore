using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class RecaptchaV2Checkbox
    {
        public readonly Versions.RecaptchaV2Checkbox Model;

        public RecaptchaV2Checkbox(Versions.RecaptchaV2Checkbox model)
        {
            if (model.Settings == null)
                throw new ArgumentException("Settings can't be null.");

            var defaultModel = new Versions.RecaptchaV2Checkbox();
            if (model.Uid.Equals(Guid.Empty))
                model.Uid = defaultModel.Uid;

            if (string.IsNullOrEmpty(model.Language))
                model.Language = defaultModel.Language;

            if (string.IsNullOrEmpty(model.Id))
                model.Id = defaultModel.Id;

            if (string.IsNullOrEmpty(model.Theme))
                model.Theme = defaultModel.Theme;

            Model = model;
        }
    }
}
