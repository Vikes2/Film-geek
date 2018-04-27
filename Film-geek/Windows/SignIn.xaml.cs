﻿using Film_geek.Classes;
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

        public SignIn()
        {
            InitializeComponent();
            User u = new User();
            u.Nickname = "Lysy";
            ListUsers.Add(u);
            u = new User();
            u.Nickname = "Stary";
            ListUsers.Add(u);
            LB_Users.ItemsSource = ListUsers;
        }

        private void User_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            User user = ((Grid)sender).Tag as User;

            PasswordInput dialog = new PasswordInput();
            dialog.User = user;
            if (dialog.ShowDialog() == true)
            {
                // wybrano OK
                Close();
                Overview overview = new Overview(user);
                overview.Show();
                
            }
            else
            {
                // wybrano Anuluj
            }


        }
    }
}
