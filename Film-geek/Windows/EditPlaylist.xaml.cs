using Film_geek.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logika interakcji dla klasy EditPlaylist.xaml
    /// </summary>
    public partial class EditPlaylist : Window
    {
        private ObservableCollection<Film> films;
        public int IdPlaylist { get; set; }
        public EditPlaylist(ObservableCollection<Film> _films)
        {
            InitializeComponent();
            films = _films;
        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BTN_Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LB_FilmViews.ItemsSource = films;
            //MessageBox.Show(films[0].Title);
        }
    }
}
