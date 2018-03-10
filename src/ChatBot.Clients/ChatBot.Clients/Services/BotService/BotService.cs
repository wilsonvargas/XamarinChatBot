using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.Clients.Services.BotService
{
    public class BotService : IBotService
    {

        #region Properties

        private HttpClient _cliente;

        public HttpClient Client
        {
            get { return _cliente; }
            set { _cliente = value; }
        }


        #endregion
        public Task Connect()
        {
            throw new NotImplementedException();
        }

        public Task SendMessage()
        {
            throw new NotImplementedException();
        }

        private static async Task<string> PostAsync(string uri, HttpContent content)
        {
            try
            {
                HttpResponseMessage response = await startConversationClient.PostAsync(uri, content);

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
