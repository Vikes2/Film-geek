using Film_geek.Classes;
using Film_geek.Util;
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

        public List<string> genresList = new List<string>();


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

            genresList.Insert(0, "Brak filtru");
            foreach (FilmGenre fg in ((App)Application.Current).AllGenres)
            {
                genresList.Add(fg.Name);
            }
            CB_genresFilter.ItemsSource = genresList;


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
                    //MessageBox.Show("Error");
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
            //((App)Application.Current).PlaylistView.CB_Playlists.SelectedIndex = 0;
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


        #region filtry/sort


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
                return (ListCollectionView)CollectionViewSource.GetDefaultView(allFilms);
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
            if (View != null)
            {
                View.SortDescriptions.Clear();
                View.CustomSort = null;
            }

        }

        



        private void Filter()
        {

            if (CB_genresFilter == null)
            {
                return;
            }

            if (int.TryParse(CB_genresFilter.SelectedIndex.ToString(), out int selectedgenres) == false)
            {
                return;
            }

            FilmGenre selectedObjGenre = null;



            if (selectedgenres > 0)
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



            ComboBoxItem cbItem = CB_ratingFilter.SelectedItem as ComboBoxItem;
            if (cbItem == null)
            {
                return;
            }

            if (int.TryParse(cbItem.Content.ToString(), out int idRating) == false)
            {
                idRating = -1;
            }


            // idRating wartość CB brak = -1, 1 = 1
            // selectedgenre index kategori 0 - brak, 1 - horror ...


            if (idRating == -1 && selectedgenres == 0)
            {
                View.Filter = null;
                return;
            }
            else if (idRating != -1 && selectedgenres == 0)
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
            else if (idRating == -1 && selectedgenres != 0)
            {
                View.Filter = delegate (object item)
                {
                    if (item is Film film)
                    {
                        if (selectedObjGenre != null)
                        {
                            foreach (FilmGenre f in film.Genres)
                            {
                                if (f.Name == selectedObjGenre.Name)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                };
            }
            else if (idRating != -1 && selectedgenres != 0)
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

        }




        private void CB_runFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }


        public void ClearFilters()
        {
            CB_genresFilter.SelectedIndex = 0;
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
