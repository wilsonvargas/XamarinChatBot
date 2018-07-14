using ChatBot.Clients.Helpers;
using ChatBot.Clients.Models;
using ChatBot.Clients.Services.BotService;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(BotService))]

namespace ChatBot.Clients.Services.BotService
{
    public class BotService : IBotService
    {
        private string botUriChat = "";

        #region Properties

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

        public BotMessage Conversation
        {
            get { return _conversation; }
            set { _conversation = value; }
        }

        private HttpClient _client;
        private BotMessage _conversation;

        #endregion Properties

        public async Task<Activity> Connect()
        {
            try
            {
                StringContent content = new StringContent("", Encoding.UTF8, "application/json");
                string result = await PostAsync(AppSettings.BaseBotEndPointAddress, content);
                var conversationResponse = JsonConvert.DeserializeObject<Conversation>(result);
                botUriChat = String.Format(AppSettings.BaseBotUriChat, conversationResponse.ConversationId);
                Settings.ConversationId = conversationResponse.ConversationId;
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
                    return await Connect();
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

        private static Activity SetExceptionMessage()
        {
            return new Activity()
            {
                Text = "Something went wrong!",
                From = new User() { Id = "", Name = "ChatBotXamarin" }
            };
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
    }
}
