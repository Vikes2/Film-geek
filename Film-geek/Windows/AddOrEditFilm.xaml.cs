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
    /// 

    public partial class AddOrEditFilm : Window
    {
        private List<GenreDic> genres;

        public Film ActiveFilm { get; set; }

        public AddOrEditFilm()
        {
            InitializeComponent();

            ActiveFilm = new Film();
            ActiveFilm.Directors = new List<Director>();

            List<FilmGenre> allGenres = ((App)Application.Current).AllGenres;
            genres = new List<GenreDic>();


            foreach (var genre in allGenres)
            {
                genres.Add(new GenreDic() { Name = genre.Name, Value = ActiveFilm.Genres.Contains(genre) });
            }
            CB_Genre.ItemsSource = genres;
        }

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
            foreach(GenreDic gd in genres)
            {
                if (gd.Value == true)
                {
                    ActiveFilm.Genres.Add(new FilmGenre() { Name = gd.Name });
                }
            }
            DialogResult = true;
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GD_ValuesGrid.DataContext = ActiveFilm;
        }
    }

    public class GenreDic
    {
        public string Name { get; set; }
        public bool Value { get; set; }

    }
}
