using ChatBot.Server.Extensions;
using ChatBot.Server.Helpers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Threading.Tasks;

namespace ChatBot.Server.Dialogs
{
    [Serializable]
    [LuisModel("HERE_YOUR_APP_KEY", "HERE_YOUR_APP_KEY")]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("Farewell")]
        public async Task Farewell(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Farewell;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Greeting;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
        }

        [LuisIntent("Location")]
        public async Task Location(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Location;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Default;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
        }

        [LuisIntent("Parking")]
        public async Task Parking(IDialogContext context, LuisResult result)
        {
            try
            {
                var response = ChatResponse.Parking;

                await context.PostAsync(await response.ToUserLocale(context));

                context.Wait(MessageReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [LuisIntent("Delivery")]
        public async Task Restaurant(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Delivery;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
        }

        [LuisIntent("Thanks")]
        public async Task Thanks(IDialogContext context, LuisResult result)
        {
            try
            {
                var response = ChatResponse.Thanks;

                await context.PostAsync(await response.ToUserLocale(context));

                context.Wait(MessageReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [LuisIntent("Wifi")]
        public async Task Wifi(IDialogContext context, LuisResult result)
        {
            try
            {
                var response = ChatResponse.Wifi;

                await context.PostAsync(await response.ToUserLocale(context));

                context.Wait(MessageReceived);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
