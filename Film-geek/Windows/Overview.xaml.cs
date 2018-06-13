using Film_geek.Classes;
using Film_geek.UserControls;
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
    /// Logika interakcji dla klasy Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        public User LoggedUser { get; set; }

        public OverviewUC OUC{ get; }
        public PlaylistView PUC { get; }

        public Overview()
        {
            InitializeComponent();
            LoggedUser = Auth.Instance.LoggedUser;

            OUC = new OverviewUC();
            PUC = new PlaylistView();

            GD_Content.Children.Add(OUC);

            ((App)Application.Current).Overview = this;
        }

        private void AddFilm_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            AddOrEditFilm addWindow = new AddOrEditFilm();
            if (addWindow.ShowDialog() == true)
            {
                Auth.Instance.AddNewFilm(addWindow.ActiveFilm);
                //window.ActiveFilm - to add to selected playlists
            }
        }

        private void AddFilm_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void LogOut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
            Auth.Instance.LogOut();
            ((App)Application.Current).SignIn.Show();
        }

        private void LogOut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Profile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Profile Window = new Profile();
            Window.Owner = this;
            Window.User = Auth.Instance.LoggedUser;
            if (Window.ShowDialog() == true)
            {
                Window.SaveAvatar();
                Auth.Instance.SaveUsers();
            }
        }

        private void Profile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Help window = new Help();
            window.Show();
        }

        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Detail_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is Film film)
            {
                FilmDetails window = new FilmDetails();
                window.Film = film;
                window.Show();
            }else
            {
                MessageBox.Show("film jest nullem");
            }

        }

        private void Detail_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditFilm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(e.Parameter is Film film)
            {
                AddOrEditFilm addOrEditWindow = new AddOrEditFilm();
                addOrEditWindow.ActiveFilm = film;
                
                if(addOrEditWindow.ShowDialog() == true)
                {
                    Auth.Instance.SavePlaylists();
                   
                }
            }
        }

        private void EditFilm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
