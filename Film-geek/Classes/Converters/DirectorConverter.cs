using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Film_geek.Classes.Converters
{
    public class DirectorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string output = "";
                foreach (Director x in (List<Director>)value)
                    output += x.ToString() + ",";
                return output;
            }
            catch (NullReferenceException)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string obj = (string)value;
            List<Director> newList = new List<Director>();
            string[] sepString = obj.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string x in sepString)
            {
                string[] sepString2 = x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                newList.Add(new Director(sepString2[0], sepString2[1]));
            }
            return newList;
        }
    }
}
