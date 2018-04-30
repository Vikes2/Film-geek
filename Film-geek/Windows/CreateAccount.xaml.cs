using Film_geek.Classes;
using Microsoft.Win32;
using Film_geek.Classes.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy create_account.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public User NewUser { get; set; }

        private ProfileSerializer<User> us;
        
        public CreateAccount()
        {
            InitializeComponent();
            NewUser = new User();
        }
        
        private void BTN_ok_Click(object sender, RoutedEventArgs e)
        {
            #region setPassword
            if (TB_passwd.Password == null || TB_passwd.Password.Length < 4)
            {
                MessageBox.Show("Hasło musi mieć minimum 4 znaki!");
                return;
            }
            else if (TB_passwd.Password != TB_passwd2.Password)
            {
                MessageBox.Show("Hasła się nie zgadzają!");
                return;
            }
            NewUser.Password = TB_passwd.Password;
            #endregion
            DialogResult = true;
            Close();
        }

        private void BTN_setPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog AvatarPicker = new OpenFileDialog
            {
                Filter = "Zdjęcia (*.png;*.jpeg)|*.png;*.jpeg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (AvatarPicker.ShowDialog() == true)
            {
                string dir = @"..\..\Resources\Avatars\" + NewUser.Nickname + ".jpg";
                
                File.Copy(AvatarPicker.FileName, dir);
                NewUser.ImagePath = dir;
            }
        }

        private void CreateAccountWindow_Loaded(object sender, RoutedEventArgs e)
        {
            us = new ProfileSerializer<User>("profiles/users", "users", ((App)Application.Current).ListUsers);
            DataGrid.DataContext = NewUser;
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
