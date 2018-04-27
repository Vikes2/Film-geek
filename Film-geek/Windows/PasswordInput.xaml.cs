using Film_geek.Classes;
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
    /// Logika interakcji dla klasy PasswordInput.xaml
    /// </summary>
    public partial class PasswordInput : Window
    {
        public User User { get; set; }
        public string Password { get; set; }

        public PasswordInput()
        {
            InitializeComponent();
        }

        private void BTN_LogIn_Click(object sender, RoutedEventArgs e)
        {
            // czy pass ok
            DialogResult = true;
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
