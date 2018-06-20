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
                ((App)Application.Current).PlaylistView.Filter();

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

        private void BTN_addPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var test2 = Auth.Instance.LoggedUser.Playlists;

            string name = "";
            int listCount = 0;
            foreach(var playlist in Auth.Instance.LoggedUser.Playlists)
            {
                if (playlist.Name.Contains("NowaPlaylista"))
                {
                    listCount++;
                }
            }
            if(listCount == 0)
            {
                name = "NowaPlaylista";
            }
            else
            {
                name = "NowaPlaylista(" + listCount + ")";
            }
            var test = Auth.Instance.LoggedUser.Playlists;
            Auth.Instance.AddNewPlaylist(name);
            var test1 = Auth.Instance.LoggedUser.Playlists;


            LB_PlaylistsView.ItemsSource = null;

            ((App)Application.Current).Playlists.Clear();
            foreach (Playlist p in Auth.Instance.LoggedUser.Playlists)
            {
                if (p.Id != 1)
                {
                    ((App)Application.Current).Playlists.Add(p);
                }

            }

            LB_PlaylistsView.ItemsSource = ((App)Application.Current).Playlists;

            int index = ((App)Application.Current).PlaylistView.CB_playlistFilter.SelectedIndex;

            ((App)Application.Current).PlaylistView.CB_playlistFilter.ItemsSource = null;
            ((App)Application.Current).PlaylistView.CB_playlistFilter.ItemsSource = Auth.Instance.LoggedUser.Playlists;
            ((App)Application.Current).PlaylistView.CB_playlistFilter.SelectedIndex = index;
            ((App)Application.Current).PlaylistView.FilmsList = Auth.Instance.LoggedUser.Playlists[0].Films;

            ((App)Application.Current).PlaylistView.Filter();

        }
    }
}
