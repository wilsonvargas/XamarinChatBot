namespace ChatBot.Clients.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new ChatBot.Clients.App());
        }
    }
}
