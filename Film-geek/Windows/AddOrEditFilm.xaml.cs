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
            public string Name { get; set; }
            public bool Value { get; set; }
        }
        private List<GenreDic> genres;
        
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
            //test data
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Horror z pawelskim" });

            InitializeComponent();        
            ActiveFilm = new Film();
            
            //GD_ValuesGrid.DataContext = ActiveFilm;

            // dev
            List<FilmGenre> allGenres = ((App)Application.Current).AllGenres;
            genres = new List<GenreDic>();

            foreach(var genre in allGenres)
            {
                genres.Add(new GenreDic() { Name = genre.Name, Value = ActiveFilm.Genres.Contains(genre) });
            }//potrzebny odpowiedni duplikat
            CB_Genre.ItemsSource = genres;
            
            //endev
        }
        
        public AddOrEditFilm(Film NewFilm)
        {
            isEditing = true;
            ActiveFilm = NewFilm;
            SL_Rating.DataContext = Auth.Instance.LoggedUser.Rating[ActiveFilm];//? i identyko z WatchStatus
            InitializeComponent();
        }

        bool isEditing = false;
        public Film ActiveFilm { get; set; }

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
            //DialogResult = true;//który to kurwa mądry to wkleił?
            if (!isEditing)
            {
                Auth.Instance.LoggedUser.Playlists[0].Films.Add(ActiveFilm);
                Auth.Instance.LoggedUser.Rating.Add(ActiveFilm, (float)SL_Rating.Value);
                bool ws = false;
                if (RB_Watched_Y.IsChecked == true)
                    ws = true;
                Auth.Instance.LoggedUser.WatchStatus.Add(ActiveFilm, ws);
            }            
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CB_Genre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                if (!(CB_Genre.Text == ""))
                    if (!(((App)Application.Current).AllGenres.Contains(new FilmGenre() {Name = CB_Genre.Text })))
                    {
                        FilmGenre newGenre = new FilmGenre() { Name = CB_Genre.Text };
                        ((App)Application.Current).AllGenres.Add(newGenre);//genres czy globalna?
                        genres.Add(new GenreDic() { Name = newGenre.Name, Value = true });
                        ActiveFilm.Genres.Add(newGenre);
                        CB_Genre.ItemsSource = null;
                        CB_Genre.ItemsSource = genres;
                    }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GD_ValuesGrid.DataContext = ActiveFilm;
        }

        private void CHB_GenreItem_Checked(object sender, RoutedEventArgs e)
        {
            List<FilmGenre> deletable = new List<FilmGenre>();
            CheckBox cb = (sender as CheckBox);
            if (cb.IsChecked == true)
                ActiveFilm.Genres.Add(new FilmGenre() { Name = cb.Name });
            else
            {
                foreach (FilmGenre item in ActiveFilm.Genres)
                    if (item.Name == cb.Name)
                        deletable.Add(item);
                foreach (FilmGenre item in deletable)
                    ActiveFilm.Genres.Remove(item);
            }        
        }
    }

    

}