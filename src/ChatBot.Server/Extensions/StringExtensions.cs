using ChatBot.Server.Services.AnalyticsService;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatBot.Server.Extensions
{
    public static class StringExtensions
    {
        public static async Task<string> ToUserLocale(this string text, IDialogContext context)
        {
            context.UserData.TryGetValue(AppSettings.UserLanguageKey, out string userLanguageCode);

            text = await TranslateService.Translate(text, AppSettings.DefaultLanguage, userLanguageCode);

            return text;
        }
    }
}