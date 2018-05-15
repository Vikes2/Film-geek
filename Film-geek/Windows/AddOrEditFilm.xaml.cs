using Film_geek.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<GenreDic> genres;

        public AddOrEditFilm()
        {
            InitializeComponent();

            #region Testowe dane
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
            #endregion

            List<FilmGenre> allGenres = ((App)Application.Current).AllGenres;
            genres = new List<GenreDic>();

            foreach(var genre in allGenres)
            {
                genres.Add(new GenreDic() { Name = genre.Name, Value = ActiveFilm.Genres.Contains(genre) });
            }
            CB_Genre.ItemsSource = genres;
        }
        
        public AddOrEditFilm(Film NewFilm)
        {
            ActiveFilm = NewFilm;
            InitializeComponent();
            GD_ValuesGrid.DataContext = ActiveFilm;
        }


        public Film ActiveFilm { get; set; }
        private FilmGenre film;

        private void BTN_ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog imagePickerDialog = new OpenFileDialog
            {
                Filter = "Zdjęcia (*.png;*.jpeg)|*.png;*.jpeg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (imagePickerDialog.ShowDialog() == true)
            {
                string dir = @"..\..\Resources\Images\" + ActiveFilm.Title + ".jpg";
                File.Copy(imagePickerDialog.FileName, dir);
                ActiveFilm.ImagePath = dir;
            }
        }

        private void BTN_Confirm_Click(object sender, RoutedEventArgs e)
        {
            // walidacja 
            DialogResult = true;
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class GenreDic
    {
        public string Name { get; set; }
        public bool Value { get; set; }
    }
}
