using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.Services.BotService;
using Xamarin.Forms;

namespace ChatBot.Clients.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        IBotService service;
        private string _greeting;

        public string Greeting
        {
            get { return _greeting; }
            set { _greeting = value; }
        }
        public Command LoadCommand
        {
            get;
            set;
        }


        public ChatViewModel(IBotService service) {
            this.service = service;
            LoadCommand = new Command(async () => await Load());
        }

        private async Task Load()
        {
            try
            {
                IsBusy = true;
                var greeting = await service.Connect();
                Greeting = greeting.Text;
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                throw;
            }
        }
    }
}
