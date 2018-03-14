using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.Models;
using ChatBot.Clients.Services.BotService;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(BotService))]
namespace ChatBot.Clients.Services.BotService
{
    public class BotService : IBotService
    {
        private StringContent content = new StringContent("", Encoding.UTF8, "application/json");
        private string botUriChat = "https://directline.botframework.com/v3/directline/conversations/{0}/activities";
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

        private HttpClient _clientChat;

        public HttpClient ClientChat
        {
            get
            {
                if (_clientChat == null)
                {
                    _clientChat = new HttpClient();
                    _clientChat.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppSettings.DirectLineKey);
                }
                return _clientChat;
            }
        }

        private Conversation conversation;

        public Conversation LastConversation
        {
            get { return conversation; }
            set { conversation = value; }
        }



        #endregion

        public async Task<Activity> Connect()
        {
            try
            {            
                string result = await PostAsync(AppSettings.BaseBotEndPointAddress, content);
                var conversationResponse = JsonConvert.DeserializeObject<Conversation>(result);
                botUriChat = String.Format("https://directline.botframework.com/v3/directline/conversations/{0}/activities", conversationResponse.ConversationId);
                
                var activitiesReceived = await Client.GetAsync(botUriChat);
                var activitiesReceivedData = await activitiesReceived.Content.ReadAsStringAsync();

                var activities = JsonConvert.DeserializeObject<BotMessage>(activitiesReceivedData);

                var activity = activities.Activities.FirstOrDefault();
                return activity;
            }
            catch (Exception ex)
            {
                return SetExceptionMessage();
            }
        }

        public async Task<Activity> SendMessage(string message)
        { 
            var postResult = JsonConvert.DeserializeObject<ConversationId>(await PostAsync(botUriChat, content));
            string jsonResultFromBot = await Client.GetStringAsync(botUriChat + "?watermark=322");
            var botMessage = JsonConvert.DeserializeObject<BotMessage>(jsonResultFromBot);
            throw new NotImplementedException();
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
                From = new User() { Id = "", Name = "ChatBotXamarin"}
            };
        }

    }
}
