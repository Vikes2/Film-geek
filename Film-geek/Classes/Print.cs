using Film_geek.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Film_geek.Classes
{
    class Print
    {
        public static void PrintFilmDetails(Film film)
        {
            FilmDetails filmWindow = new FilmDetails();
            filmWindow.Film = film;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                printDialog.PrintVisual(filmWindow.ContentContainer, "Film Details");

        }
    }
}
