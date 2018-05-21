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
    /// Logika interakcji dla klasy FilmDetails.xaml
    /// </summary>
    public partial class FilmDetails : Window
    {
        private Film film;
        public Film Film
        {
            get
            {
                return film;
            }
          set
            {
                film = value;
            }
        }
        public FilmDetails()
        {
            InitializeComponent();

        }


        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_FilmEdit_Click(object sender, RoutedEventArgs e)
        {
            // addOrEditFilm(film)
        }

        private void BTN_FilmPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region setImage
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(film.ImagePath, UriKind.Relative);
            bimage.EndInit();
            IMG_FilmImage.Source = bimage;
            #endregion
            LBL_FilmTitle.Content = film.Title;
            TBL_FilmGenres.Text = film.GenresDetails();
            if(((App)Application.Current).Overview.LoggedUser.Rating.Keys.Contains<Film>(film) == true)
            {
                SL_Rating.Value = (double)((App)Application.Current).Overview.LoggedUser.Rating[film];
            }
            else
            {
                SL_Rating.Value = 0;
            }
            #region setIsWatched
            if(((App)Application.Current).Overview.LoggedUser.WatchStatus.Keys.Contains(film) == true)
            {
                CHB_IsWatched.IsChecked = ((App)Application.Current).Overview.LoggedUser.WatchStatus[film];

            }
            else
            {
                CHB_IsWatched.IsChecked = false;
            }

            if (CHB_IsWatched.IsChecked == true)
                CHB_IsWatched.Content = "Obejrzane";
            else
                CHB_IsWatched.Content = "Nie obejrzane";
            #endregion
            LBL_FilmDirector.Content = film.DirectorsDetails();
            LBL_FilmActors.Content = film.ActorsDetails();
            #region setReleaseDate
            if (film.ReleaseDate == null)
                LBL_FilmRelease.Content = "brak daty premiery";
            else
                LBL_FilmRelease.Content = String.Format("{0:MM/dd/yyyy}", film.ReleaseDate);
            #endregion
            #region setDescription
            if (film.Description == String.Empty && film.Description == null)
                TBL_FilmDescription.Text = "brak opisu";
            else
                TBL_FilmDescription.Text = film.Description;
            #endregion
        }
    }
}
