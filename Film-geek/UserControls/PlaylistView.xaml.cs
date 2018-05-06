using Film_geek.Util;
using Film_geek.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Film_geek.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
            CB_Playlists.ItemsSource = Auth.Instance.LoggedUser.Playlists;
            CB_Playlists.SelectedIndex = 0;
            UserDetails.DataContext = Auth.Instance.LoggedUser;

        }


        private void CB_Playlists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_PlaylistsView.Items.Refresh();
        }

        private void BTN_Overview_Click(object sender, RoutedEventArgs e)
        {
            Overview overview = ((App)Application.Current).Overview;
            overview.GD_Content.Children.Clear();
            overview.GD_Content.Children.Add(overview.OUC);
        }
    }
}
