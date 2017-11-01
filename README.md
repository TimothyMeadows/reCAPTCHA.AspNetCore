# reCAPTCHA.AspNetCore
Google reCAPTCHA for ASP.NET Core 2

# Requirements
Because this is a google service. You must first have a **secret key** and a **site key** in order to use the reCAPTCHA service. You can read more about reCAPTCHA as well as sign up for free here: https://www.google.com/recaptcha/intro/

# Configure

Right click on your project file and goto Manage Secrets.

This will open secrets.json. Add the follow entry to the file make sure to paste in your secret key and site key:
```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here"
  } 
```

Now open Startup.cs and add the following code as shown below to your ConfigureServices method:

```csharp
services.Configure<RecaptchaSettings>(Configuration.GetSection("RecaptchaSettings"));
services.AddTransient<IRecaptchaService, RecaptchaService>();
```

# Usage

In order to prevent having to copy and paste your site key all over your view files (a nightmare to update later). You can inject your settings from the Startup method by adding the following code to top of your view file:

```csharp
@inject IOptions<RecaptchaSettings> RecaptchaSettings
```

You can then freely include the Recaptcha script inside of forms you wish to vaidate later in your controller (supports multiple forms).
```csharp
@using (Html.BeginForm("SomeMethod", "SomeController")) {
  @Html.Recaptcha(RecaptchaSettings.Value.SiteKey)
}
```

# Validation

In order to validate a recaptcha script being used in a form you will first need to inject the IRecaptchaService class into your controller using the code below:

```csharp
private IRecaptchaService _recaptcha;

public SomeController(IRecaptchaService recaptcha)
{
  _recaptcha = recaptcha;
}
```

Finally you can validate the recaptcha attempts using the Validate method in the Recaptcha service in your HttpPost method:

```csharp
public async Task<IActionResult> SomeMethod(SomeModel model)
{
  var valid = await _recaptcha.Validate(Request);
  return View();
}
```
