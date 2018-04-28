using Film_geek.Classes;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Film_geek.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy PlaylistOverview.xaml
    /// </summary>
    public partial class PlaylistOverview : Window
    {
        public User LoggedUser { get; set; }

        public PlaylistOverview(User user)
        {
            InitializeComponent();
            LoggedUser = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserDetails.DataContext = LoggedUser;
        }
    }
}
