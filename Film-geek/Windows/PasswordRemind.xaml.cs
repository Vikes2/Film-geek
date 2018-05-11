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
    /// Logika interakcji dla klasy PasswordRemind.xaml
    /// </summary>
    public partial class PasswordRemind : Window
    {
        User contextUser;
        public PasswordRemind()
        {
            InitializeComponent();
        }

        public PasswordRemind(User u)
        {
            InitializeComponent();
            contextUser = u;

        }

        private void PasswordRemind_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.DataContext = contextUser;
        }

        private void TB_answer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TB_answer.Text == contextUser.SecurityAnswer)
            {
                TB_passwd.IsEnabled = true;
                TB_passwd2.IsEnabled = true;
                BTN_OK.IsEnabled = true;
            }
            else
            {
                TB_passwd.IsEnabled = false;
                TB_passwd2.IsEnabled = false;
                BTN_OK.IsEnabled = false;
            }
        }

        private void BTN_OK_Click(object sender, RoutedEventArgs e)
        {
            if (TB_passwd.Password.Length < 4)
            {
                MessageBox.Show("Hasło musi mieć minimum 4 znaki!");
                return;
            }

            if (TB_passwd.Password != TB_passwd2.Password)
            {
                MessageBox.Show("Hasła się nie zgadzają!");
                return;
            }
            DialogResult = true;
            Close();
            contextUser.Password = (new PasswordEncoder()).EncryptWithByteArray(TB_passwd.Password);
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
