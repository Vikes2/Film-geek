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
        private OpenFileDialog avatarPicker;

        public User NewUser { get; set; }
        
        public CreateAccount()
        {
            InitializeComponent();
            NewUser = new User();
        }

        public void SaveAvatar()
        {
            if(avatarPicker != null)
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars", NewUser.Id + System.IO.Path.GetExtension(avatarPicker.FileName));
                if (File.Exists(path))
                    File.Delete(path);
                File.Copy(avatarPicker.FileName, path);
                NewUser.ImagePath = path;
            }
        }
        
        private void BTN_ok_Click(object sender, RoutedEventArgs e)
        {
            #region setPassword
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
            NewUser.Password = TB_passwd.Password;
            #endregion
            SaveAvatar();
            DialogResult = true;
            Close();
        }

        private void BTN_setPhoto_Click(object sender, RoutedEventArgs e)
        {
            avatarPicker = new OpenFileDialog
            {
                Filter = "Zdjęcia (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*jpg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (avatarPicker.ShowDialog() == true)
            {
                NewUser.ImagePath = avatarPicker.FileName;
            }

        }

        private void CreateAccountWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.DataContext = NewUser;
            TB_login.Focus();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
