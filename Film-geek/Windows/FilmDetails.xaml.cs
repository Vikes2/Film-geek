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

        public FilmDetails(Film f)
        {
            film = f;
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
            Print.PrintFilmDetails(Film);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region setImage
            BitmapImage bimage = new BitmapImage();
            bimage.BeginInit();
            bimage.UriSource = new Uri(film.ImagePath, UriKind.RelativeOrAbsolute);
            bimage.EndInit();
            IMG_FilmImage.Source = bimage;
            #endregion
            LBL_FilmTitle.Content = film.Title;
            TBL_FilmGenres.Text = film.GenresDetails();

            LBL_FilmDirector.Content = film.DirectorsDetails();
            LBL_FilmActors.Content = film.ActorsDetails();
        }
    }
}
