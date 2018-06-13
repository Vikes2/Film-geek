using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Film_geek.Classes.Converters
{
    public class DirectorConverter : IValueConverter //do poprawy
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
            foreach (string x in sepString)
            {
                string[] sepString2 = x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (sepString2.Count() == 1)
                {
                    newList.Add(new Director("", sepString2[0]));
                    continue;
                }
                string fullName = "";
                for (int i = 1; i < sepString2.Count(); i++)
                    fullName += sepString2[i] + " ";
                fullName.Remove(fullName.Length - 1);//?
                newList.Add(new Director(sepString2[0], fullName));
            }
            return newList;
        }
    }
}
