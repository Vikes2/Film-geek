﻿using Film_geek.Classes;
using Film_geek.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string Nickname { get; set; }
        public string ImagePath { get; set; }
        public ObservableCollection<Playlist> Playlists { get; set; }
        public Dictionary<Film, float> Rating { get; set; }



        public Profile()
        {
            InitializeComponent();
        }

        public void SaveAvatar()
        {
            if (avatarPicker != null)
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars", Nickname + System.IO.Path.GetExtension(avatarPicker.FileName));
                File.Copy(avatarPicker.FileName, path);
                ImagePath = path;
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
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars", Nickname + System.IO.Path.GetExtension(avatarPicker.FileName));
                ImagePath = avatarPicker.FileName;
            }

        }

        private void BTN_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void BTN_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void ProfileWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Nickname = Auth.Instance.LoggedUser.Nickname;
            ImagePath = Auth.Instance.LoggedUser.ImagePath;
            //avatarPicker.FileName = ImagePath;
            //IMG_UserImage.Source = ImagePath;
        }

        private void TB_login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
