using Film_geek.Classes;
using Film_geek.UserControls;
using Film_geek.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Film_geek
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {

        public Overview Overview { get; set; }
        public SignIn SignIn { get; set; }
        public PlaylistView PlaylistView { get; set; }
        public EditPlaylist EditPlaylist { get; set; }

        private ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();
        public ObservableCollection<Playlist> Playlists
        {
            get
            {
                return playlists;
            }
            set
            {
                playlists = value;
            }
        }

        




        private List<FilmGenre> allGenres = new List<FilmGenre>();

        public List<FilmGenre> AllGenres
        {
            get
            {
                return allGenres;
            }
            set
            {
                allGenres = value;
            }
        }


    }
}
