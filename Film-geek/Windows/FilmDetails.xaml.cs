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
        public FilmDetails()
        {
            InitializeComponent();
            //  bajzel do testowania
            FilmGenre a = new FilmGenre() { Name = "horror" };
            FilmGenre b = new FilmGenre() { Name = "cartoon" };
            FilmGenre c = new FilmGenre() { Name = "drama" };
            ((App)Application.Current).AllGenres.Add(a);
            ((App)Application.Current).AllGenres.Add(b);

            Film film = new Film();
            Director d = new Director("Kłentin", "Tarantino");
            Actor act = new Actor("Stiwen", "Sigal");
            film.Genres = new List<FilmGenre>(((App)Application.Current).AllGenres);
            film.Title = "Potwór w pokoju pracy";
            film.Description = "straszny film o strasznie obrazonym wojtku";
          //  film.Directors.Add(d);
            film.Actors.Add(act);
            DateTime dat = new DateTime(2001, 09, 11);
            film.ReleaseDate = dat;


            ((App)Application.Current).AllGenres.Add(c);
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(film.ImagePath, UriKind.Relative);
            bimage.EndInit();

            IMG_FilmImage.Source = bimage;
            LBL_FilmTitle.Content = film.Title;
            TBL_FilmGenres.Text = film.GenresDetails();
            //  SL_Rating.Value = (double)((App)Application.Current).Overview.LoggedUser.Rating[film];
            // CHB_IsWatched.IsChecked = ((App)Application.Current).Overview.LoggedUser.WatchStatus[film];
            LBL_FilmDirector.Content = film.DirectorsDetails();
            LBL_FilmActors.Content = film.ActorsDetails();
            LBL_FilmRelease.Content = String.Format("{0:MM/dd/yyyy}", film.ReleaseDate); //a to nie wiem czy zadziala
            TBL_FilmDescription.Text = film.Description;
        }

        public FilmDetails(Film film)
        {
            InitializeComponent();
            #region setImage
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(film.ImagePath, UriKind.Relative);
            bimage.EndInit();
            IMG_FilmImage.Source = bimage;
            #endregion
            LBL_FilmTitle.Content = film.Title;
            TBL_FilmGenres.Text = film.GenresDetails();
            SL_Rating.Value = (double)((App)Application.Current).Overview.LoggedUser.Rating[film];
            #region setIsWatched
            CHB_IsWatched.IsChecked = ((App)Application.Current).Overview.LoggedUser.WatchStatus[film];
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
        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTN_FilmEdit_Click(object sender, RoutedEventArgs e)
        {
            // addOrEditFilm(film)
        }
    }
}
