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
    /// Logika interakcji dla klasy ProfilesView.xaml
    /// </summary>
    public partial class ProfilesView : UserControl
    {
        private SignIn signInWindow;

        public ProfilesView()
        {
            InitializeComponent();
            signInWindow = ((App)Application.Current).SignIn;

            LB_Users.ItemsSource = signInWindow.ListUsers;

        }


        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            User user = ((Grid)sender).Tag as User;
            //((App)Application.Current).LoggedUser = user;

            signInWindow.GD_SignInContent.Children.Clear();
            signInWindow.PasswordView.User = user;
            signInWindow.GD_SignInContent.Children.Add(signInWindow.PasswordView);

            //PasswordInput dialog = new PasswordInput();
            //dialog.User = user;
            //if (dialog.ShowDialog() == true)
            //{
            //    // wybrano OK
            //    Close();
            //    Overview overview = new Overview(user);
            //    overview.Show();

            //}
            //else
            //{
            //    // wybrano Anuluj
            //}


        }

    }
}
