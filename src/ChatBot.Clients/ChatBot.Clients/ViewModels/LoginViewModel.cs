using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatBot.Clients.Helpers;
using ChatBot.Clients.Views;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace ChatBot.Clients.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Properties
        private string _username;

        public string UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
    
        public ICommand LoginCommand { get; set; }

        #endregion

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());            
        }

        private async Task Login()
        {
            IsBusy = true;
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    Settings.IsLogin = true;
                    Settings.UserName = UserName;
                    Application.Current.MainPage = new ChatView();
                }
                else
                {
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error de conexión", "Tu conexión a internet está fallando", "Ok");
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                ErrorMessage = ex.Message;
            }
        }

    }
}
