using Film_geek.Classes;
using Film_geek.Classes.Serializer;
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
using System.Windows.Media.Animation;
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

        private User clickedUser;

        public ProfilesView()
        {
            InitializeComponent();
            signInWindow = ((App)Application.Current).SignIn;
        }

        private void ShowPasswordWindow(object sender, EventArgs e)
        {
            LB_Users.SelectedItem = null;
            signInWindow.GD_SignInContent.Children.Clear();
            signInWindow.PasswordView.User = clickedUser;
            signInWindow.GD_SignInContent.Children.Add(signInWindow.PasswordView);

            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 1;
            anim.Duration = TimeSpan.FromSeconds(0.3);
            signInWindow.GD_SignInContent.BeginAnimation(Grid.OpacityProperty, anim);
        
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clickedUser = ((Grid)sender).Tag as User;

            DoubleAnimation anim = new DoubleAnimation();
            anim.To = 0;
            anim.Duration = TimeSpan.FromSeconds(0.3);
            anim.Completed += ShowPasswordWindow;
            signInWindow.GD_SignInContent.BeginAnimation(Grid.OpacityProperty, anim);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LB_Users.ItemsSource = Auth.Instance.users;
            signInWindow.LB_InfoBar.Content = "Witaj w aplikacji. Wybierz profil.";

        }

        private void BTN_NewUser_Click(object sender, RoutedEventArgs e)
        {
            User newUser;
            CreateAccount createAccountWindow = new CreateAccount();
            createAccountWindow.Owner = signInWindow;
            if (createAccountWindow.ShowDialog() == true)
            {
                newUser = createAccountWindow.NewUser;
                Auth.Instance.AddNewUser(newUser);
            }
        }
    }
}
