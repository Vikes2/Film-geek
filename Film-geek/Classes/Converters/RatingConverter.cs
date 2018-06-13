using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Film_geek.Classes.Converters
{
    class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rating = (double)value;

            return (rating / 0.2).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double.TryParse(((ComboBoxItem)value).Content.ToString(), out double rating);

            MessageBox.Show(rating.ToString());
            if (rating >= 0 && rating <= 5)
            {
                return rating * 0.2;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
