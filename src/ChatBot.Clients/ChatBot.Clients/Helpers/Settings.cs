using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ChatBot.Clients.Helpers
{
    /// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters.
	/// </summary>
	public static class Settings
    {
        public static string ConversationId
        {
            get
            {
                return AppSettings.GetValueOrDefault(ConversationIdKey, ConversationIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ConversationIdKey, value);
            }
        }

        public static bool IsLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsLoginKey, IsLoginDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsLoginKey, value);
            }
        }

        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string ConversationIdKey = "conversation_key";
        private const string IsLoginKey = "login_key";
        private const string UserNameKey = "username_key";
        private static readonly string ConversationIdDefault = "";
        private static readonly bool IsLoginDefault = false;
        private static readonly string UserNameDefault = "";

        #endregion Setting Constants
    }
}
