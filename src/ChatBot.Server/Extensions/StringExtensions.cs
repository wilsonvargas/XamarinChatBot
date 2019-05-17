using ChatBot.Server.Services.TranslateService;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;

namespace ChatBot.Server.Extensions
{
    public static class StringExtensions
    {
        public static async Task<string> ToUserLocale(this string text, IDialogContext context)
        {
            context.UserData.TryGetValue(AppSettings.UserLanguageKey, out string userLanguageCode);

            text = await TranslateService.TranslateAsync(text, AppSettings.DefaultLanguage, userLanguageCode);

            return text;
        }
    }
}
