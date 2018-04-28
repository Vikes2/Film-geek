using Film_geek.Classes;
using Film_geek.UserControls;
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
    /// Logika interakcji dla klasy SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public List<User> ListUsers = new List<User>();

        public ProfilesView ProfilesView { get; set; }
        public PasswordInputView PasswordView { get; set; }

        public SignIn()
        {
            InitializeComponent();

            #region test users
            User u = new User();
            u.Nickname = "Lysy";
            ListUsers.Add(u);
            u = new User();
            u.Nickname = "Stary";
            ListUsers.Add(u);
            u = new User();
            u.Nickname = "Ostry";
            ListUsers.Add(u);
            u = new User();
            u.Nickname = "Tepy";
            ListUsers.Add(u);
            u = new User();
            u.Nickname = "Łagodny";
            ListUsers.Add(u);
            #endregion

            ProfilesView = new ProfilesView();
            PasswordView = new PasswordInputView();
            GD_SignInContent.Children.Add(ProfilesView);


        }

    }
}
