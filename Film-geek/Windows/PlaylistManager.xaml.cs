using Film_geek.Classes;
using Film_geek.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy PlaylistManager.xaml
    /// </summary>
    public partial class PlaylistManager : Window
    {
        public PlaylistManager()
        {
            InitializeComponent();
        }

        private void BTN_del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć listę?\nObiekt zostanie trwale usuniety.",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Playlist playlist = (Playlist)((Button)sender).Tag;
                Auth.Instance.DeletePlaylist(playlist);
            }
        }

        private void BTN_edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Playlist> playlists = Auth.Instance.LoggedUser.Playlists;
            playlists.RemoveAt(0);

            LB_PlaylistsView.ItemsSource = playlists;
        }
    }
}
