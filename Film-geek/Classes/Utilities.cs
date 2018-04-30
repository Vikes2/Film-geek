using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Film_geek.Classes
{
    class Utilities
    {
        public static Window GetWindowRef(string WindowName)
        {
            Window retVal = null;
            foreach (Window window in Application.Current.Windows)
            {
                // The window's Name is set in XAML. See comment below
                if (window.Name.Trim().ToLower() == WindowName.Trim().ToLower())
                {
                    retVal = window;
                    break;
                }
            }

            return retVal;
        }

    }

    public class BoolToOppositeBoolConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
