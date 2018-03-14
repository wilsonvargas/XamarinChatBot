using System;
using System.Collections.Generic;
using System.Text;
using ChatBot.Clients.Helpers;
using ChatBot.Clients.Models;
using Xamarin.Forms;

namespace ChatBot.Clients.Converters
{
    public class TypeToColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            User user = (User) value;
            var localUser = Settings.UserName;
            int column = 0;
            if (user.Name != localUser)
            {
                column = 0;
            }
            else
            {
                column = 2;
            }
            return column;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
