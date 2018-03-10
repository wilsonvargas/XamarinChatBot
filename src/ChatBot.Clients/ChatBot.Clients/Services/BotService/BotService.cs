using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        #region Properties

        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    _client.BaseAddress = new Uri(AppSettings.BaseBotEndPointAddress);
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + AppSettings.DirectLineKey);
                }
                return _client;
            }
        }

        private Conversation conversation;

        public Conversation LastConversation
        {
            get { return conversation; }
            set { conversation = value; }
        }



        #endregion

        public async Task<MessageSet> Connect()
        {
            try
            {
                var response = await Client.GetAsync("/api/tokens/");
                if (response.IsSuccessStatusCode)
                {
                    var conversation = new Conversation();
                    HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(conversation), Encoding.UTF8,
                        "application/json");
                    response = await _client.PostAsync("/api/conversations/", contentPost);
                    if (response.IsSuccessStatusCode)
                    {
                        var conversationInfo = await response.Content.ReadAsStringAsync();
                        LastConversation = JsonConvert.DeserializeObject<Conversation>(conversationInfo);
                        var conversationUrl = $"{AppSettings.BaseBotEndPointAddress}/api/conversations/{LastConversation.ConversationId}/messages/";


                        var resp = await _client.PostAsync(conversationUrl, contentPost);
                        var messageInfo = await response.Content.ReadAsStringAsync();

                        var messagesReceived = await _client.GetAsync(conversationUrl);
                        var messagesReceivedData = await messagesReceived.Content.ReadAsStringAsync();
                        var messages = JsonConvert.DeserializeObject<MessageSet>(messagesReceivedData);

                        return messages;
                    }
                }
                return SetExceptionMessage();
            }
            catch (Exception ex)
            {
                return SetExceptionMessage();
            }
        }

        public async Task<MessageSet> SendMessage(string message)
        {
            try
            {
                var messageToSend = new BotMessage() { Text = message, ConversationId = LastConversation.ConversationId };
                var contentPost = new StringContent(JsonConvert.SerializeObject(messageToSend), Encoding.UTF8, "application/json");
                var conversationUrl = $"{AppSettings.BaseBotEndPointAddress}/api/conversations/{LastConversation.ConversationId}/messages/";


                var response = await _client.PostAsync(conversationUrl, contentPost);
                var messageInfo = await response.Content.ReadAsStringAsync();

                var messagesReceived = await _client.GetAsync(conversationUrl);
                var messagesReceivedData = await messagesReceived.Content.ReadAsStringAsync();

                var messages = JsonConvert.DeserializeObject<MessageSet>(messagesReceivedData);

                return messages;
            }
            catch (Exception ex)
            {
                return SetExceptionMessage();
            }
        }

        private static MessageSet SetExceptionMessage()
        {
            return new MessageSet()
            {
                Messages = (new List<BotMessage>
                {
                    new BotMessage
                    {
                        Text = "Something went wrong!",
                        From = "Bot"
                    }
                })
            };
        }

    }
}
