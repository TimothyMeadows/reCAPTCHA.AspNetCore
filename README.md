# reCAPTCHA.AspNetCore
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![nuget](https://img.shields.io/nuget/v/reCAPTCHA.AspNetCore.svg)](https://www.nuget.org/packages/reCAPTCHA.AspNetCore/)

Google reCAPTCHA v2/v3 for .NET Standard 2.0, ASP.NET Core 2, and .NET Framework.

# Install

From the a command prompt
```bash
dotnet add package reCAPTCHA.AspNetCore
```

```bash
Install-Package reCAPTCHA.AspNetCore
```

You can also search for package via your nuget ui / website:

https://www.nuget.org/packages/reCAPTCHA.AspNetCore/

# Requirements
You must first have a **secret key** and a **site key** in order to use the reCAPTCHA service. This package supports v2 and v3 api keys. You can read more about reCAPTCHA v2, and v3 as well as sign up for free here: https://www.google.com/recaptcha/intro/

# Configure

Choose how you want to configure the storage of your RecaptchaSettings. This contains your site key, and site secret so it's recommended to use secrets.json with Azure Key Vault (or similar setup). However you can also just add the section to your appconfig.json file.

# Versions

- [v2](https://developers.google.com/recaptcha/docs/display)
- [v2-invis](https://developers.google.com/recaptcha/docs/invisible)
- [v3](https://developers.google.com/recaptcha/docs/v3)

#### appconfig.json

Add the follow entry to the file make sure to paste in your secret key and site key followed by setting the correct version to v2 or v3 depending on your key type:
```json
"RecaptchaSettings": {
    "Enabled": true,
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here",
    "Version": "v2"
  } 
```

#### secrets.json
Right click on your project file and goto Manage Secrets.

This will open secrets.json. Add the follow entry to the file make sure to paste in your secret key and site key followed by setting the correct version to v2 or v3 depending on your key type:
```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here",
    "Version": "v2"
  } 
```

Note: This will also require you to have a setup such as Azure Key Vault (or similar setup) when running in production.

# Examples

Open Startup.cs and add the following code as shown below to your ConfigureServices method:

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
  @Html.Recaptcha(RecaptchaSettings.Value)
}
```

If you wish to trigger a JavaScript function on callback you can pass a method name to the Html helper.
```csharp
@using (Html.BeginForm("SomeMethod", "SomeController")) {
  @Html.Recaptcha(RecaptchaSettings.Value, successCallback:"methodName")
}
```
```html
<script>
  function methodName() {
    alert('Hello world!');
  }
</script>
```

You may find that you need to add a reference to Microsoft.Extensions.Options before you can use IOptions.

```csharp
@using Microsoft.Extensions.Options
@using reCAPTCHA.AspNetCore
```

You can see a tested example of usage in the [Contact.cshtml](https://github.com/TimothyMeadows/reCAPTCHA.AspNetCore/blob/master/reCAPTCHA.AspNetCore.Example/Views/Home/Contact.cshtml) view. However you will need to configure it with your key information before running yourself. You should also take note of the allowed domains security policy in the Google Recaptcha docs.

# Validation

You can validate the recaptcha attempts using the ValidateRecaptchaAttribute on your HttpPost method:

```csharp
[HttpPost]
[ValidateRecaptcha]
public async Task<IActionResult> SomeMethod(SomeModel model)
{
  return View(model);
}
```

You can see a tested example of usage in the [HomeController.cs](https://github.com/TimothyMeadows/reCAPTCHA.AspNetCore/blob/master/reCAPTCHA.AspNetCore.Example/Controllers/HomeController.cs) controller. However you will need to configure it with your key information before running yourself. You should also take note of the allowed domains security policy in the Google Recaptcha docs.

# Recaptcha.net

Users who suffer from censorship concerns can now bypass the libraries default of www.google.com to www.recaptcha.net using the following static variable in the RecaptchaService class. You can best set this static during the ConfigureService method.

```csharp
RecapthaService.UseRecapchaNet = true; // Default is false
```
