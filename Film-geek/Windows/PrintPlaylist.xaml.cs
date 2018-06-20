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
    /// Logika interakcji dla klasy PrintPlaylist.xaml
    /// </summary>
    public partial class PrintPlaylist : Window
    {
        public PrintPlaylist()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LB_PlaylistsView.ItemsSource = ((App)Application.Current).Overview.PUC.LB_PlaylistsView.ItemsSource;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(LB_PlaylistsView, "A Simple Drawing");
                Close();
            }
        }
    }
}
