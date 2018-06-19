using Film_geek.Classes;
using Film_geek.Util;
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


        private void BTN_FilmPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(ContentContainer, "A Simple Drawing");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ContentContainer.DataContext = Film;
            SL_Rating.Value = Film.Rating;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddOrEditFilm addOrEditWindow = new AddOrEditFilm();
            addOrEditWindow.ActiveFilm = film;

            if (addOrEditWindow.ShowDialog() == true)
            {
                Auth.Instance.SavePlaylists();
            }
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
