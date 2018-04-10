# Integrating Bot Framework with Xamarin.Form

This is a small sample application showing how to integrate Bot Framework with Xamarin.Forms via Direct Line.
This is sample explained in a post for my Spanish blog: 

* [Integrating Bot Framework with Xamarin.Forms - Azure Setup](https://blog.wilsonvargas.com/integrando-bot-framework-con-xamarin-forms-parte-1/)
* [Integrating Bot Framework with Xamarin.Forms - Xamarin Deploy](https://blog.wilsonvargas.com/integrando-bot-framework-con-xamarin-forms-parte-2/)

Built with C# 6 features, you must be running VS 2015 or VS 2017 to compile.

Built with Xamarin.Forms with support for:

    iOS
    Android

Build status: [![Build status](https://ci.appveyor.com/api/projects/status/672e7943803fw3ft?svg=true)](https://ci.appveyor.com/project/wilsonvargas/xamarinchatbot)



![image](https://raw.githubusercontent.com/wilsonvargas/XamarinChatBot/master/images/image.png)


## Available content:

You can find the following content related to this sample:

> Some content is available in Spanish.

* [Creating your free Azure account](https://azure.microsoft.com/en-us/free/)
* [Integrating Bot Framework with Xamarin.Forms - Azure Setup](https://blog.wilsonvargas.com/integrando-bot-framework-con-xamarin-forms-parte-1/)
* [Integrating Bot Framework with Xamarin.Forms - Xamarin Deploy](https://blog.wilsonvargas.com/integrando-bot-framework-con-xamarin-forms-parte-2/)


## Setup
For security reasons I use a file called [AppSettings.cs.dist](/src/ChatBot.Server/AppSettings.cs.dist) you must change their name to **AppSettings.cs** and complete with your credentials obtained in your azure account:

### Server project

```cs
public static class AppSettings
{
    public static readonly string TranslatorUriBase = "https://api.microsofttranslator.com/V2/Http.svc/";
    public static readonly string TranslatorKey = "";
    public static readonly string DefaultLanguage = "en";
    public static readonly string UserLanguageKey = "LanguageCode";
    public static readonly string OcpApimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
}
```

### Client project 
[AppSettings.cs.dist](/src/ChatBot.Clients/ChatBot.Clients/AppSettings.cs.dist)

```cs
public static class AppSettings
{
    public static readonly string DirectLineKey = "";
    public static readonly string BaseBotEndPointAddress = "https://directline.botframework.com/v3/directline/conversations/";
}
```


