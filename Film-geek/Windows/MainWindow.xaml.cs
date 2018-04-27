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

namespace Film_geek.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void sigin_Click(object sender, RoutedEventArgs e)
        {
            SignIn window = new SignIn();
            window.Show();
            
        }

        private void create_account_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount window = new CreateAccount();
            window.Show();
        }

        private void overview_Click(object sender, RoutedEventArgs e)
        {
            Overview window = new Overview();
            window.Show();
        }
    }
}
