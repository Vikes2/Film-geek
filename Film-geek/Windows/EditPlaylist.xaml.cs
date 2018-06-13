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
    /// Logika interakcji dla klasy EditPlaylist.xaml
    /// </summary>
    public partial class EditPlaylist : Window
    {
        public ObservableCollection<Film> films;
        public ObservableCollection<Film> allFilms;
        public ObservableCollection<Film> diff;

        public int IdPlaylist { get; set; }
        private Playlist MyPlaylist;
        private bool currentFilms;
        public bool CurrentFilms
        {
            get
            {
                return currentFilms;
            }
            set
            {
                currentFilms = value;
            }
        }
        public EditPlaylist(ObservableCollection<Film> _films)
        {
            InitializeComponent();
            films = new ObservableCollection<Film>();
            allFilms = new ObservableCollection<Film>();
            diff = new ObservableCollection<Film>();
            currentFilms = true;

            loadPlaylists(_films);


            ((App)Application.Current).EditPlaylist = this;
        }
        private void loadPlaylists(ObservableCollection<Film> _films)
        {
            foreach (Film film in _films)
            {
                films.Add(film);
            }
            foreach (Film film in Auth.Instance.LoggedUser.Playlists[0].Films)
            {
                film.isShowing = false;
                allFilms.Add(film);
            }

            foreach (Film f in allFilms)
            {
                diff.Add(f);
                foreach (Film film in films)
                {
                    if (film.Id == f.Id)
                    {
                        diff.Remove(f);
                        continue;
                    }
                }
            }

            syncLists();

            setMyPlaylist();

        }

        private void syncLists()
        {
            foreach (Film f in allFilms)
            {
                if(diff.Contains(f) && !films.Contains(f))
                {
                    f.isShowing = false;
                }else if(films.Contains(f) && !diff.Contains(f))
                {
                    f.isShowing = true;
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void setMyPlaylist()
        {
            foreach (Playlist p in Auth.Instance.LoggedUser.Playlists)
            {
                if(p.Id == IdPlaylist)
                {
                    MyPlaylist = p;
                    break;
                }
            }
        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).PlaylistView.CB_Playlists.SelectedIndex = 0;
            DialogResult = true;
            Close();
        }

        private void BTN_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LB_ExFilmViews.ItemsSource = allFilms;
            CurrentFilms = true;

            

        }

        private void CHKB_isInclude_Checked(object sender, RoutedEventArgs e)
        {
            // adding to playlist
            Film film = (Film)(((CheckBox)sender).Tag);
            films.Add(film);
            diff.Remove(film);
            syncLists();

        }

        private void CHKB_isInclude_Unchecked(object sender, RoutedEventArgs e)
        {
            Film film = (Film)(((CheckBox)sender).Tag);
            diff.Add(film);
            films.Remove(film);
            syncLists();
        }
    }
}
