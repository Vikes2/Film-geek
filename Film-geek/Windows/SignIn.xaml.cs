﻿using Film_geek.Classes;
using Film_geek.Classes.Serializer;
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
        private ProfileSerializer<User> us;

        public ProfilesView ProfilesView { get; set; }
        public PasswordInputView PasswordView { get; set; }

        public SignIn()
        {
            InitializeComponent();
            ((App)Application.Current).SignIn = this;

            ProfilesView = new ProfilesView();
            PasswordView = new PasswordInputView();
            GD_SignInContent.Children.Add(ProfilesView);
        }

        private void SignInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //us = new ProfileSerializer<User>("profiles/users", "users", ((App)Application.Current).ListUsers);
            //((App)Application.Current).ListUsers = us.PullData();
        }
    }
}
