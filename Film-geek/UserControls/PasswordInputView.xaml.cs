using Film_geek.Classes;
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
    /// Logika interakcji dla klasy PasswordInputView.xaml
    /// </summary>
    public partial class PasswordInputView : UserControl
    {
        public User User { get; set; }
        private SignIn signInWindow;
        public string Password { get; set; }

        public PasswordInputView()
        {
            InitializeComponent();
            signInWindow = ((App)Application.Current).SignIn;
        }


        private void BTN_LogIn_Click(object sender, RoutedEventArgs e)
        {
            PasswordEncoder pe = new PasswordEncoder();
            if (User.Password == pe.EncryptWithByteArray(TB_Password.Password))
            {
                ((App)Application.Current).LoggedUser = User;
                signInWindow.Hide();
                Overview overview = new Overview();
                ((App)Application.Current).Overview = overview;
                overview.Show();
            }
            else
            {
                MessageBox.Show("Podano nieprawidłowe hasło.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {

            signInWindow.GD_SignInContent.Children.Clear();
            signInWindow.GD_SignInContent.Children.Add(signInWindow.ProfilesView);

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GD_userContext.DataContext = User;
            signInWindow.LB_InfoBar.Content = "Podaj hasło. Hasło: 1234.";
        }
    }
}
