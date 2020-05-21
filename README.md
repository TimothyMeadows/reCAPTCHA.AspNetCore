# reCAPTCHA.AspNetCore
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![nuget](https://img.shields.io/nuget/v/reCAPTCHA.AspNetCore.svg)](https://www.nuget.org/packages/reCAPTCHA.AspNetCore/)

Google reCAPTCHA for .NET Core 3.x. The older .NET Core 2.x version can be found [here](https://github.com/TimothyMeadows/reCAPTCHA.AspNetCore/tree/2.x).

*Note: There have been changes to this libraries structure between versions 2, and 3. If you still wish to use version 2 it's been frozen at version 2.2.5 on nuget.*

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

Choose how you want to configure the storage of your ```RecaptchaSettings```. This contains your site key, and site secret so it's recommended to use ```secrets.json``` with Azure Key Vault (or similar setup). However you can also just add the section to your ```appconfig.json``` file.


#### appconfig.json

Add the follow entry to the file make sure to paste in your secret key and site key followed by setting the correct version to v2 or v3 depending on your key type:
```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here"
  } 
```

#### secrets.json
Right click on your project file and goto Manage Secrets.

This will open secrets.json. Add the follow entry to the file make sure to paste in your secret key and site key followed by setting the correct version to v2 or v3 depending on your key type:
```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here"
  } 
```

*Note: This will also require you to have a setup such as Azure Key Vault (or similar setup) when running in production.*

#### Content Security Policy

If you use a content security policy you can specify the values for script-src, and frame-src using the below example. Note that you should also make sure the Site option used for those who suffer from censorship matches the values you are using. The default value for Site is www.google.com.

```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here",
    "ContentSecurityPolicy": "https://www.google.com/recaptcha/"
  } 
```

This is an example for those that have to use recaptcha.net which would also have to change the site value:

```json
"RecaptchaSettings": {
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here",
    "Site": "www.recaptcha.net",
    "ContentSecurityPolicy": "https://www.recaptcha.net/recaptcha/"
  } 
```

# Versions

These are the currently supported versions. Below is also the list of class names for T when using ```Html.Recaptcha<T>```

- [RecaptchaV2Checkbox](https://developers.google.com/recaptcha/docs/display)
- [RecaptchaV2Invisible](https://developers.google.com/recaptcha/docs/invisible)
- [RecaptchaV3HiddenInput](https://developers.google.com/recaptcha/docs/v3)

# Examples

Open `Startup.cs` and add the following code as shown below to your `ConfigureServices` method:

```csharp
// Add recaptcha and pass recaptcha configuration section
services.AddRecaptcha(Configuration.GetSection("RecaptchaSettings"));

// Or configure recaptcha via options
services.AddRecaptcha(options =>
{
    options.SecretKey = "Your secret key";
    options.SiteKey = "Your site key";
});
```

# Usage

In order to prevent having to copy and paste your site key all over your view files (a nightmare to update later). You can inject your settings from the Startup method by adding the following code to top of your view file:

```csharp
@inject IOptions<RecaptchaSettings> RecaptchaSettings
```

You can then freely include the Recaptcha script inside of forms you wish to vaidate later in your controller (supports multiple forms).
```csharp
@using (Html.BeginForm("SomeMethod", "SomeController")) {
  @(Html.Recaptcha<RecaptchaV2Checkbox>(RecaptchaSettings?.Value))
}
```

If you wish to trigger a JavaScript function on callback you can pass a method name to the Html helper.
```csharp
@using (Html.BeginForm("SomeMethod", "SomeController")) {
  @(Html.Recaptcha<RecaptchaV2Checkbox>(RecaptchaSettings?.Value, new RecaptchaV2Checkbox { successCallback = "methodName" }))
}
```
```html
<script>
  function methodName() {
    alert('caw caw caw!');
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

You can also specify a minimum score you wish to accept when a success occurs:

```csharp
[HttpPost]
[ValidateRecaptcha(0.5)]
public async Task<IActionResult> SomeMethod(SomeModel model)
{
  return View(model);
}
```

You can see a tested example of usage in the [HomeController.cs](https://github.com/TimothyMeadows/reCAPTCHA.AspNetCore/blob/master/reCAPTCHA.AspNetCore.Example/Controllers/HomeController.cs) controller. However you will need to configure it with your key information before running yourself. You should also take note of the allowed domains security policy in the Google Recaptcha docs.

# Recaptcha.net

Users who suffer from censorship concerns can now bypass the libraries default of www.google.com to www.recaptcha.net, or a proxy of there choosing using the following optional setting in ```appsettings.json```.

```csharp
"RecaptchaSettings": {
    "Site": "www.recaptcha.net",
    "SecretKey": "paste secret key here",
    "SiteKey": "paste site key here"
  }
```
