using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatBot.Clients.Helpers;
using ChatBot.Clients.Views;
using Xamarin.Forms;

namespace ChatBot.Clients
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            if (Settings.IsLogin)
            {
                MainPage = new NavigationPage(new ChatView());
            }
            else
            {
                MainPage = new NavigationPage(new LoginView());
            }
			
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
