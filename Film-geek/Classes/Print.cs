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
        public static void Func()
        {
            PrintDialog printDialog = new PrintDialog();
            Label a = new Label();
            a.Content = "rucham psa jak sra";
            if(printDialog.ShowDialog() == true)
                printDialog.PrintVisual(a,"aaa");
        }

        public static void PrintFilmDetails()
        {
            FilmDetails filmWindow = new FilmDetails();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                printDialog.PrintVisual(filmWindow.ContentContainer, "aaa");

        }
    }
}
