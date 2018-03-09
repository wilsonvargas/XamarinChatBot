using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using ChatBot.Server.Dialogs;
using ChatBot.Server.Helpers;
using ChatBot.Server.Models;
using ChatBot.Server.Services.TranslateService;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;

namespace ChatBot.Server
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                if (!string.IsNullOrEmpty(activity.Text))
                {
                    //detect language of input text
                    var userLanguage = await TranslateService.DetermineLanguageAsync(activity.Text);
                    //save user's LanguageCode to Azure Table Storage
                    var message = activity as IMessageActivity;
                    try
                    {
                        using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                        {
                            var botDataStore = scope.Resolve<IBotDataStore<BotData>>();
                            var key = new AddressKey()
                            {
                                BotId = message.Recipient.Id,
                                ChannelId = message.ChannelId,
                                UserId = message.From.Id,
                                ConversationId = message.Conversation.Id,
                                ServiceUrl = message.ServiceUrl
                            };


                            var userData = await botDataStore.LoadAsync(key, BotStoreType.BotUserData, CancellationToken.None);

                            var storedLanguageCode = userData.GetProperty<string>(AppSettings.UserLanguageKey);

                            //update user's language in Azure Table Storage
                            if (storedLanguageCode != userLanguage)
                            {
                                userData.SetProperty(AppSettings.UserLanguageKey, userLanguage);
                                await botDataStore.SaveAsync(key, BotStoreType.BotUserData, userData, CancellationToken.None);
                                await botDataStore.FlushAsync(key, CancellationToken.None);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    activity.Text = await TranslateService.TranslateTextToDefaultLanguage(activity, userLanguage);
                    await Conversation.SendAsync(activity, MakeRoot);
                }
            }
            else
            {
                await HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        internal static IDialog<object> MakeRoot()
        {
            try
            {
                return Chain.From(() => new RootDialog());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task<Activity> HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                IConversationUpdateActivity update = message;

                var client = new ConnectorClient(new Uri(message.ServiceUrl), new MicrosoftAppCredentials());
                if (update.MembersAdded != null && update.MembersAdded.Any())
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id == message.Recipient.Id)
                        {
                            var reply = message.CreateReply();
                            reply.Text = $"Hi {newMember.Name}, " + ChatResponse.Greeting;
                            await client.Conversations.ReplyToActivityAsync(reply);
                        }
                    }
                }
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}