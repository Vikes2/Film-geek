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
