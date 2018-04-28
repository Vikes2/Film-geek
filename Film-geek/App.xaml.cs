using Film_geek.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Film_geek
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
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
