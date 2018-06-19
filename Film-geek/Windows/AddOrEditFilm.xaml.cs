using Film_geek.Classes;
using Film_geek.Util;
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

        public class GenreDic
        {
            public FilmGenre Genre { get; set; }
            public bool Value { get; set; }

            public override string ToString()
            {
                return Genre.Name;
            }
        }
        private List<GenreDic> genres;

        public Film ActiveFilm { get; set; }
        
        private Dictionary<FilmGenre, bool> filmGenres = new Dictionary<FilmGenre, bool>();
        private OpenFileDialog imagePicker;

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

            ActiveFilm = new Film();
            ActiveFilm.Directors = new List<Director>();            

        }

        private void SaveImage()
        {
            if (imagePicker != null)
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Covers", ActiveFilm.Title + System.IO.Path.GetExtension(imagePicker.FileName));
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                        File.Copy(imagePicker.FileName, path);
                        ActiveFilm.ImagePath = path;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void BTN_ImagePicker_Click(object sender, RoutedEventArgs e)
        {
            imagePicker = new OpenFileDialog
            {
                Filter = "Zdjęcia (*.png;*.jpeg)|*.png;*.jpeg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (imagePicker.ShowDialog() == true)
            {
                ActiveFilm.ImagePath = imagePicker.FileName;
            }
        }

        private void BTN_Confirm_Click(object sender, RoutedEventArgs e)
        {
            List<FilmGenre> tmp = new List<FilmGenre>();
            foreach(GenreDic gd in genres)
            {
                if (gd.Value == true)
                {
                    tmp.Add(new FilmGenre() { Name = gd.Genre.Name });
                }
            }

            ActiveFilm.Genres = tmp;

            (TB_Name.GetBindingExpression(TextBox.TextProperty)).UpdateSource();
            (TB_Description.GetBindingExpression(TextBox.TextProperty)).UpdateSource();
            (TB_Actors.GetBindingExpression(TextBox.TextProperty)).UpdateSource();
            (TB_Directors.GetBindingExpression(TextBox.TextProperty)).UpdateSource();
            
            DialogResult = true;
            SaveImage();
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GD_ValuesGrid.DataContext = ActiveFilm;

            List<FilmGenre> allGenres = ((App)Application.Current).AllGenres;
            SL_Rating.Value = ActiveFilm.Rating;

            genres = new List<GenreDic>();

            foreach (var genre in allGenres)
            {
                GenreDic gd = new GenreDic();
                gd.Genre = genre;
                gd.Value = false;
                foreach (var filmGenre in ActiveFilm.Genres)
                {
                    if (genre.Name == filmGenre.Name)
                    {
                        gd.Value = true;
                        break;
                    }
                }
                genres.Add(gd);
            }

            CB_Genre.ItemsSource = genres;
        }
    }
}