using Film_geek.Util;
using Film_geek.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Film_geek.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Overview.xaml
    /// </summary>
    public partial class OverviewUC : UserControl
    {
        public OverviewUC()
        {
            InitializeComponent();
        }


        private void BTN_PlaylistView_Click(object sender, RoutedEventArgs e)
        {
            Overview overview = ((App)Application.Current).Overview;
            overview.GD_Content.Children.Clear();
            overview.GD_Content.Children.Add(overview.PUC);
        }

        private void OverviewUC_Loaded(object sender, RoutedEventArgs e)
        {
            GD_UserDetails.DataContext = Auth.Instance.LoggedUser;

            #region testowy
            //Auth.Instance.AddNewPlaylist("Na wieczory przy winie");
            //Auth.Instance.AddNewPlaylist("Dla dzieci");
            //Auth.Instance.AddNewPlaylist("Luźne");
            //Auth.Instance.AddNewPlaylist("Krótkie");
            //Auth.Instance.AddNewPlaylist("Długie");

            //Auth.Instance.AddNewFilm(new Classes.Film()
            //{
            //    Title = "Incepcja",
            //    Playlists = new List<int>() { 2, 6}
            //});

            //Auth.Instance.AddNewFilm(new Classes.Film()
            //{
            //    Title = "Opowieść Wigilijna",
            //    Playlists = new List<int>() { 3, 4, 5 }
            //});

            //Auth.Instance.AddNewFilm(new Classes.Film()
            //{
            //    Title = "2012",
            //    Playlists = new List<int>() { 6 }
            //});

            //Auth.Instance.AddNewFilm(new Classes.Film()
            //{
            //    Title = "Apollo 13",
            //    Playlists = new List<int>() { 2 }
            //});

            //Auth.Instance.AddNewFilm(new Classes.Film()
            //{
            //    Title = "Pulp Fiction",
            //    Playlists = new List<int>() { 2, 3, 6 }
            //});

            #endregion

        }

        private void BTN_AddFilm_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditFilm window = new AddOrEditFilm();
            window.Show();
        }

        private void BTN_Help_Click(object sender, RoutedEventArgs e)
        {
            Help window = new Help();
            window.Show();
        }
    }
}
