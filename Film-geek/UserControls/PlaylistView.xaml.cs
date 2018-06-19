using Film_geek.Classes;
using Film_geek.Util;
using Film_geek.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public List<string> genresList = new List<string>();

        private Film selectedFilm;

        public PlaylistView()
        {
            InitializeComponent();
            FilmsList = Auth.Instance.LoggedUser.Playlists[0].Films;
            LB_PlaylistsView.ItemsSource = FilmsList;
            //LB_PlaylistsView.ItemsSource = Auth.Instance.LoggedUser.Playlists[0].Films;
            //CB_Playlists.ItemsSource = Auth.Instance.LoggedUser.Playlists;
            //CB_Playlists.SelectedIndex = 0;
            ((App)Application.Current).PlaylistView = this;
            GD_UserDetails.DataContext = Auth.Instance.LoggedUser;
            CB_playlistFilter.ItemsSource = Auth.Instance.LoggedUser.Playlists;

            genresList.Insert(0, "Brak filtru");
            foreach(FilmGenre fg in ((App)Application.Current).AllGenres)
            {
                genresList.Add(fg.Name);
            }
            CB_genresFilter.ItemsSource = genresList;

        }

        private void BTN_Overview_Click(object sender, RoutedEventArgs e)
        {
            Overview overview = ((App)Application.Current).Overview;
            overview.GD_Content.Children.Clear();
            overview.GD_Content.Children.Add(overview.OUC);
        }

        public void refreshListSource()
        {
            LB_PlaylistsView.ItemsSource = null;
            FilmsList = Auth.Instance.LoggedUser.Playlists[0].Films;
            LB_PlaylistsView.ItemsSource = FilmsList;
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

            Filter();
        }

        private void deleteFilm(object sender, RoutedEventArgs e)
        {
            Film film = (Film)((Button)sender).Tag;
            Playlist playlist = CB_playlistFilter.SelectedItem as Playlist;
            if(playlist != null)
            {

                Auth.Instance.DeleteFilm(film, playlist);
            }

            Filter();

        }

        private void BTN_PlaylistManager_Click(object sender, RoutedEventArgs e)
        {
            PlaylistManager window = new PlaylistManager();
            window.Owner = ((App)Application.Current).Overview;
            window.Show();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ExcludedPlaylists = new ObservableCollection<Playlist>();

            LB_ButtonsView.ItemsSource = ExcludedPlaylists;

        }

        #region
        //-------------sortowanie



        private ObservableCollection<Film> filmsList;
        public ObservableCollection<Film> FilmsList
        {
            get
            {
                return filmsList;
            }
            set
            {
                filmsList = value;
            }
        }

        private ListCollectionView View
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(FilmsList);
            }
        }
        private class SortByTitleLengthDesc : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                Film film_x = (Film)x;
                Film film_y = (Film)y;
                return film_x.Title.Length.CompareTo(film_y.Title.Length) * -1;
            }
        }
        private void SortTitleLengthDesc(object sender, RoutedEventArgs e)
        {
            View.CustomSort = new SortByTitleLengthDesc();
        }

        private class SortByTitleLength : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                Film film_x = (Film)x;
                Film film_y = (Film)y;
                return film_x.Title.Length.CompareTo(film_y.Title.Length);
            }
        }
        private void SortTitleLength(object sender, RoutedEventArgs e)
        {
            View.CustomSort = new SortByTitleLength();
        }
        private void SortTitle(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        private void SortRank(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Rating", ListSortDirection.Ascending));
        }

        private void SortRankDesc(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Rating", ListSortDirection.Descending));
        }

        private void SortTitleDesc(object sender, RoutedEventArgs e)
        {
            View.SortDescriptions.Clear();
            View.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Descending));
        }

        private void SortNone(object sender, RoutedEventArgs e)
        {
            if(View != null)
            {
                View.SortDescriptions.Clear();
                View.CustomSort = null;
            }

        }

        

        // ------------------------filtry


        public void Filter()
        {

            if(CB_genresFilter == null)
            {
                return;
            }

            if (int.TryParse(CB_genresFilter.SelectedIndex.ToString(), out int selectedgenres) == false)
            {
                return;
            }

            FilmGenre selectedObjGenre = null;



            if ( selectedgenres >0)
            {
                foreach (FilmGenre fg in ((App)Application.Current).AllGenres)
                {
                    if (fg.Name == genresList[selectedgenres]) 
                    {
                        selectedObjGenre = fg;
                        break;
                    }
                }

            }


            Playlist playlist = CB_playlistFilter.SelectedItem as Playlist;

            if(playlist == null)
            {
                return;
            }

            int idPlaylist = playlist.Id; 

            ComboBoxItem cbItem = CB_ratingFilter.SelectedItem as ComboBoxItem;
            if(cbItem == null)
            {
                return;
            }
            
            if (int.TryParse(cbItem.Content.ToString(), out int idRating) == false)
            {
                idRating = -1;
            }


            // idPlaylist id playlisty (1= wszystkie, 2- fajne)
            // idRating wartość CB brak = -1, 1 = 1
            // selectedgenre index kategori 0 - brak, 1 - horror ...


            if (idPlaylist == 1 && idRating == -1 && selectedgenres == 0)
            {
                View.Filter = null;
                return;
            }
            else if (idPlaylist != 1 && idRating == -1 && selectedgenres == 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {
                        return (film.Playlists.Contains(idPlaylist));
                    }
                    return false;
                };
            }
            else if (idPlaylist == 1 && idRating != -1 && selectedgenres == 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {
                        return (film.Rating == idRating);
                    }
                    return false;
                };
            }
            else if (idPlaylist != 1 && idRating != -1 && selectedgenres == 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {
                        return (film.Rating == idRating && film.Playlists.Contains(idPlaylist));
                    }
                    return false;
                };
            }   // ======================================================================================= kategorie
            else if (idPlaylist == 1 && idRating == -1 && selectedgenres != 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {
                        if(selectedObjGenre != null)
                        {
                            foreach (FilmGenre f in film.Genres)
                            {
                                if(f.Name == selectedObjGenre.Name)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                };
            }                                                             
            else if (idPlaylist != 1 && idRating == -1 && selectedgenres != 0)
            {                                                             
                View.Filter = delegate (object item)                      
                {                                                         
                    if (item is Film film)                                
                    {

                        if (selectedObjGenre != null)
                        {
                            foreach (FilmGenre f in film.Genres)
                            {
                                if (f.Name == selectedObjGenre.Name && film.Playlists.Contains(idPlaylist))
                                {
                                    return true;
                                }
                            }
                        }
                    }                                                     
                    return false;                                         
                };                                                        
            }                                                             
            else if (idPlaylist == 1 && idRating != -1 && selectedgenres != 0)
            {                                                             
                View.Filter = delegate (object item)                      
                {                                                         
                    if (item is Film film)                                
                    {

                        if (selectedObjGenre != null)
                        {
                            foreach (FilmGenre f in film.Genres)
                            {
                                if (f.Name == selectedObjGenre.Name && film.Rating == idRating)
                                {
                                    return true;
                                }
                            }
                        }

                    }                                                     
                    return false;                                         
                };                                                        
            }                                                             
            else if (idPlaylist != 1 && idRating != -1 && selectedgenres != 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {

                        if (selectedObjGenre != null)
                        {
                            foreach (FilmGenre f in film.Genres)
                            {
                                if (f.Name == selectedObjGenre.Name && film.Rating == idRating && film.Playlists.Contains(idPlaylist))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                };
            }
        }

        private void CB_runFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        public void ClearFilters()
        {
            CB_genresFilter.SelectedIndex = 0;
            CB_playlistFilter.SelectedIndex = 0;
            CB_ratingFilter.SelectedIndex = 0;
            View.Filter = null;
            return;
        }

        private void ClearFiltrs_Click(object sender, RoutedEventArgs e)
        {
            ClearFilters();
        }
        #endregion

    }
}
