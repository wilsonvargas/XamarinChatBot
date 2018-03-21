using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.Models;
using ChatBot.Clients.Services.BotService;
using ChatBot.Clients.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatBot.Clients.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatView : ContentPage
	{
        private ChatViewModel vm;
		public ChatView ()
		{
			InitializeComponent ();
            BindingContext = vm = new ChatViewModel(DependencyService.Get<IBotService>());            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object, object>(this, "AutoScroll", (sender, arg) => {
                var message = (Activity)arg;
                messages.ScrollTo(message, ScrollToPosition.End, false);
            });
            vm.LoadCommand.Execute(null);
        }
    }
}