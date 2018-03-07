using ChatBot.Server.Extensions;
using ChatBot.Server.Helpers;
using ChatBot.Server.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatBot.Server.Dialogs
{
    [Serializable]
    [LuisModel("772681f5-1cfb-4636-abbd-9f454360996c", "81d08cadc86c493bba26ff425e8693de")]
    public class RootDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Default;

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

        [LuisIntent("Farewell")]
        public async Task Farewell(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Farewell;

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

        [LuisIntent("Delivery")]
        public async Task Restaurant(IDialogContext context, LuisResult result)
        {
            var response = ChatResponse.Delivery;

            await context.PostAsync(await response.ToUserLocale(context));

            context.Wait(MessageReceived);
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
    }
}