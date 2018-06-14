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
                ((App)Application.Current).Playlists.Remove(playlist);
                //((App)Application.Current).PlaylistView.CB_Playlists.SelectedIndex = 0;

            }
        }

        private void BTN_edit_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Film> films = ((Playlist)(((Button)sender).Tag)).Films;
            EditPlaylist window = new EditPlaylist(films);
            window.IdPlaylist = ((Playlist)(((Button)sender).Tag)).Id;
            if (window.ShowDialog() == true)
            {
                Auth.Instance.SetFilmsIntoPlaylist(window.films, window.IdPlaylist);

            }
            else
            {

            }
        }

        private void BTN_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Playlists.Clear();
            foreach (Playlist p in Auth.Instance.LoggedUser.Playlists)
            {
                if(p.Id != 1)
                {
                    ((App)Application.Current).Playlists.Add(p);
                }

            }

            LB_PlaylistsView.ItemsSource = ((App)Application.Current).Playlists;

            
        }
    }
}
