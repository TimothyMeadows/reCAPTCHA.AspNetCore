using System;
using System.Collections.Generic;
using System.Text;

namespace reCAPTCHA.AspNetCore.Templates
{
    public partial class RecaptchaV3HiddenInput
    {
        public readonly Versions.RecaptchaV3HiddenInput Model;

        public RecaptchaV3HiddenInput(Versions.RecaptchaV3HiddenInput model)
        {
            if (model.Settings == null)
                throw new ArgumentException("Settings can't be null.");

            var defaultModel = new Versions.RecaptchaV3HiddenInput();
            if (model.Uid.Equals(Guid.Empty))
                model.Uid = defaultModel.Uid;

            if (string.IsNullOrEmpty(model.Language))
                model.Language = defaultModel.Language;

            if (string.IsNullOrEmpty(model.Action))
                model.Action = defaultModel.Action;

            Model = model;
        }
    }
}
