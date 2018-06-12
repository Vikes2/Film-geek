using Film_geek.Classes;
using Film_geek.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        // impotr/export playlists, change avatar, change nickname

        private OpenFileDialog avatarPicker;  //= new OpenFileDialog();
        private string imagePath;

        public User User { get; set; }

        public Profile()
        {
            InitializeComponent();
        }

        public void SaveAvatar()
        {
            if (avatarPicker != null)
            {
                // to do: plik w uzyciu
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars", User.Id + System.IO.Path.GetExtension(avatarPicker.FileName));
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    } catch(IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                File.Copy(avatarPicker.FileName, path);
                User.ImagePath = path;
            }
        }

        private void BTN_setPhoto_Click(object sender, RoutedEventArgs e)
        {
            avatarPicker = new OpenFileDialog
            {
                Filter = "Zdjęcia (*.png;*.jpeg)|*.png;*.jpeg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (avatarPicker.ShowDialog() == true)
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars", User.Id + System.IO.Path.GetExtension(avatarPicker.FileName));
                IMG_UserImage.Source = new BitmapImage(new Uri(avatarPicker.FileName, UriKind.RelativeOrAbsolute));
            }

        }

        private void BTN_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            BindingExpression binding = TB_login.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            User = Auth.Instance.LoggedUser;
            DataGrid.DataContext = User;
        }

        private void TB_login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BTN_PasswdoChanger_Click(object sender, RoutedEventArgs e)
        {
            PasswordRemind passwordRemindWindow = new PasswordRemind(Auth.Instance.LoggedUser);
            passwordRemindWindow.Owner = this;
            if (passwordRemindWindow.ShowDialog() == true)
            {
                //kod powodzenia
            }

        }
    }
}
