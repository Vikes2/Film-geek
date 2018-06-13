using Film_geek.Classes;
using Film_geek.Util;
using Film_geek.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Film_geek.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public ObservableCollection<Playlist> ExcludedPlaylists { get; set; }

        private Film selectedFilm;

        public PlaylistView()
        {
            InitializeComponent();
            CB_Playlists.ItemsSource = Auth.Instance.LoggedUser.Playlists;
            CB_Playlists.SelectedIndex = 0;
            ((App)Application.Current).PlaylistView = this;
            GD_UserDetails.DataContext = Auth.Instance.LoggedUser;
        }

        private void BTN_Overview_Click(object sender, RoutedEventArgs e)
        {
            Overview overview = ((App)Application.Current).Overview;
            overview.GD_Content.Children.Clear();
            overview.GD_Content.Children.Add(overview.OUC);
        }

        private void updateSource(Film film)
        {
            List<Playlist> toDel = new List<Playlist>();
            ExcludedPlaylists = new ObservableCollection<Playlist>(Auth.Instance.LoggedUser.Playlists);
            foreach (int i in film.Playlists)
            {
                foreach (Playlist p in ExcludedPlaylists)
                {
                    if (p.Id == i)
                    {
                        toDel.Add(p);

                    }
                }

            }
            foreach (Playlist p in ExcludedPlaylists)
            {
                if (p.Id == 1)
                {
                    toDel.Add(p);
                    break;
                }
            }


            foreach (Playlist p in toDel)
            {
                ExcludedPlaylists.Remove(p);
            }
        }


        private void BTN_pop_Click(object sender, RoutedEventArgs e)
        {

            Film film = (Film)((Button)sender).Tag;
            if (POP_list.IsOpen == false)
            {
                updateSource(film);
                LB_ButtonsView.ItemsSource = null;
                LB_ButtonsView.ItemsSource = ExcludedPlaylists;
                selectedFilm = film;
                POP_list.IsOpen = true;
            }
            else
            {
                POP_list.IsOpen = false;
                selectedFilm = null;
            }

        }

        private void BTN_exit_Click(object sender, RoutedEventArgs e)
        {
            POP_list.IsOpen = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ExcludedPlaylists = new ObservableCollection<Playlist>();

            LB_ButtonsView.ItemsSource = ExcludedPlaylists;

        }

        private void AddToPlaylist(object sender, RoutedEventArgs e)
        {
            Playlist playlist = (Playlist)((Button)sender).Tag;
            if(selectedFilm != null)
            {
                if (!selectedFilm.Playlists.Contains(playlist.Id))
                {

                    Auth.Instance.AddFilmToPlaylist(selectedFilm, playlist);
                }
            }
            POP_list.IsOpen = false;


        }

        private void deleteFilm(object sender, RoutedEventArgs e)
        {
            Film film = (Film)((Button)sender).Tag;
            Auth.Instance.DeleteFilm(film);
        }

        private void BTN_PlaylistManager_Click(object sender, RoutedEventArgs e)
        {
            PlaylistManager window = new PlaylistManager();
            window.Owner = ((App)Application.Current).Overview;
            window.Show();
        }
    }
}
