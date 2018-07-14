using ChatBot.Clients.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatBot.Clients.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = vm = new LoginViewModel();
        }

        private LoginViewModel vm;
    }
}
