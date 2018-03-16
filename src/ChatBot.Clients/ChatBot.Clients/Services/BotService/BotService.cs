using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.Helpers;
using ChatBot.Clients.Models;
using ChatBot.Clients.Services.BotService;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(BotService))]
namespace ChatBot.Clients.Services.BotService
{
    public class BotService : IBotService
    {
        private string botUriChat = "https://directline.botframework.com/v3/directline/conversations/{0}/messages";
        private string conversationId = "";
        private BotMessage Conversation;

        #region Properties

        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppSettings.DirectLineKey);
                }
                return _client;
            }
        }

        #endregion

        public async Task<Activity> Connect()
        {
            try
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string result = await PostAsync(AppSettings.BaseBotEndPointAddress, content);
                var conversationResponse = JsonConvert.DeserializeObject<Conversation>(result);
                botUriChat = String.Format("https://directline.botframework.com/v3/directline/conversations/{0}/activities/", conversationResponse.ConversationId);
                Settings.ConversationId = conversationResponse.ConversationId;
                conversationId = conversationResponse.ConversationId;
                var activitiesReceived = await Client.GetAsync(botUriChat);
                var activitiesReceivedData = await activitiesReceived.Content.ReadAsStringAsync();

                Conversation = JsonConvert.DeserializeObject<BotMessage>(activitiesReceivedData);

                var activity = Conversation.Activities.FirstOrDefault();
                if (activity != null)
                {
                    return activity;
                }
                else
                {
                    return SetExceptionMessage();
                }

            }
            catch (Exception ex)
            {
                return SetExceptionMessage();
            }
        }

        public async Task<Activity> SendMessage(Activity message)
        {
            BotMessage conversation;
            var contentPost = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            var postResult = JsonConvert.DeserializeObject<ConversationId>(await PostAsync(botUriChat, contentPost));

            string jsonResultFromBot = await Client.GetStringAsync(botUriChat + "?watermark=" + Conversation.Watermark);
            conversation = JsonConvert.DeserializeObject<BotMessage>(jsonResultFromBot);
            if (conversation != null)
            {
                return conversation.Activities.Last();
            }
            else
            {
                return SetExceptionMessage();
            }

        }

        private async Task<string> PostAsync(string uri, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsync(uri, content);
                Stream stream = await response.Content.ReadAsStreamAsync();
                StreamReader readStream = new StreamReader(stream, Encoding.UTF8);
                string result = readStream.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static Activity SetExceptionMessage()
        {
            return new Activity()
            {

                Text = "Something went wrong!",
                From = new User() { Id = "", Name = "ChatBotXamarin" }
            };
        }

    }
}
