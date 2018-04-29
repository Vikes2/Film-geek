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
        private static bool loaduser = false; //zmienna ladowania testowego usera

        //private List<User> listUsers;
        //public List<User> ListUsers
        //{
        //    get
        //    {
        //        return listUsers;
        //    }
        //    set
        //    {
        //        listUsers = value;
        //    }
        //}

        public ProfilesView ProfilesView { get; set; }
        public PasswordInputView PasswordView { get; set; }

        public SignIn()
        {
            InitializeComponent();
            ((App)Application.Current).SignIn = this;
            //ListUsers = new List<User>();

            #region test_user_definition
            if (loaduser == false)
            {
                User u = new User();
                ((App)Application.Current).ListUsers.Add(u);
                loaduser = true;
            }
            #endregion


            ProfilesView = new ProfilesView();
            PasswordView = new PasswordInputView();
            GD_SignInContent.Children.Add(ProfilesView);


        }

    }
}
