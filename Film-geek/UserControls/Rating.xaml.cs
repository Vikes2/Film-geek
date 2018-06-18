using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Film_geek.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Rating.xaml
    /// </summary>
    public partial class Rating : UserControl
    {
        public static readonly DependencyProperty RatingProperty = DependencyProperty.Register("Value", typeof(int), typeof(Rating));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(Rating));

        public int Value
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }

        public Rating()
        {
            InitializeComponent();
        }

        private void ColourStars(int index, SolidColorBrush colour)
        {
            for (int i = 1; i <= index; i++)
            {
                foreach (Polygon s in StarContainer.Children)
                {
                    if (int.Parse(s.Tag.ToString()) <= index)
                    {
                        s.Fill = colour;
                    }
                }
            }
        }

        private void Polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            Polygon star = e.Source as Polygon;
            int.TryParse(star.Tag.ToString(), out int index);
            if(Value > index)
            {
                ColourStars(Value, null);
            }
            ColourStars(index, Brushes.Orange);
        }

        private void Polygon_MouseLeave(object sender, MouseEventArgs e)
        {
            Polygon star = e.Source as Polygon;
            int.TryParse(star.Tag.ToString(), out int index);
            if(Value > 0)
            {
                ColourStars(5, null);
                ColourStars(Value, Brushes.Orange);
            }
            else
            {
                ColourStars(5, null);
            }
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!ReadOnly)
            {
                Polygon star = e.Source as Polygon;
                int.TryParse(star.Tag.ToString(), out int index);
                ColourStars(index, Brushes.Orange);
                Value = index;
            }
        }

        private void RatingStar_Loaded(object sender, RoutedEventArgs e)
        {
            ColourStars(Value, Brushes.Orange);
        }
    }
}
