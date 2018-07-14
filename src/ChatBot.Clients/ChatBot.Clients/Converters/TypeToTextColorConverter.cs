using ChatBot.Clients.Helpers;
using ChatBot.Clients.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChatBot.Clients.Converters
{
    public class TypeToTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int parameterType = int.Parse(parameter.ToString());
            Xamarin.Forms.Color background = Color.Silver;
            User user = (User)value;
            var localUser = Settings.UserName;
            if (user.Name == localUser)
            {
                background = Color.White;
                if (parameterType == 0)
                    background = Color.Black;
            }
            else
            {
                background = Color.Black;
                if (parameterType == 0)
                    background = Color.White;
            }

            return background;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw null;
        }
    }
}
