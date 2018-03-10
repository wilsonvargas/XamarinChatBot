using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBot.Clients.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatBot.Clients.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        LoginViewModel vm;
		public LoginView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = vm = new LoginViewModel();
		}
	}
}