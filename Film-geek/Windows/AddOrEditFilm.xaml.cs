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
    /// Logika interakcji dla klasy AddOrEditFilm.xaml
    /// </summary>
    public partial class AddOrEditFilm : Window
    {
        private Dictionary<FilmGenre, bool> filmGenres = new Dictionary<FilmGenre, bool>();
        public Dictionary<FilmGenre, bool> FilmGenres
        {
            get
            {
                return filmGenres;
            }
            set
            {
                filmGenres = value;
            }
        }
        public AddOrEditFilm()
        {
            InitializeComponent();
            FilmGenre a = new FilmGenre() { Name = "horror" };
            FilmGenre b = new FilmGenre() { Name = "cartoon" };
            FilmGenre c = new FilmGenre() { Name = "drama" };
            ((App)Application.Current).AllGenres.Add(a);
            ((App)Application.Current).AllGenres.Add(b);

            ActiveFilm = new Film()
            {
                Genres = new List<FilmGenre>(((App)Application.Current).AllGenres)
            };

            ((App)Application.Current).AllGenres.Add(c);

            List<FilmGenre> genres = ((App)Application.Current).AllGenres;
            

            foreach(var genre in genres)
            {
                filmGenres[genre] = ActiveFilm.Genres.Contains(genre);
            }
            //GD_ValuesGrid.DataContext = ActiveFilm;
            CB_Genre.ItemsSource = filmGenres;
        }
        
        public AddOrEditFilm(Film NewFilm)
        {
            ActiveFilm = NewFilm;
            InitializeComponent();
            GD_ValuesGrid.DataContext = ActiveFilm;
        }

        public Film ActiveFilm;
        private FilmGenre film;
    }
}
