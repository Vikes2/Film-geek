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
        public User inputUser;
        private ProfileSerializer<User> us;
        
        public CreateAccount()
        {
            InitializeComponent();
            inputUser = new User();
            DataGrid.DataContext = inputUser;
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
            inputUser.Password = TB_passwd.Password; 
            #endregion
            ((App)Application.Current).ListUsers.Add(inputUser);
            var w = Utilities.GetWindowRef("CreateAccountWindow");
            w.Close();
            
            SignIn signWindow = new SignIn();
            signWindow.Show();
        }

        private void BTN_setPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zdjęcia (*.png;*.jpeg)|*.png;*.jpeg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                File.Copy(openFileDialog.FileName, @"..\..\Resources\Avatars\"+inputUser.Nickname+".jpg");
                inputUser.ImagePath = openFileDialog.FileName;
            }

        private void CreateAccountWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void B_ok_Click(object sender, RoutedEventArgs e)
        {
            //if (TB_passwd.Password != TB_passwd2.Password)
            //    MessageBox.Show("XD");
            //tu bedzie walidacja
            us = new ProfileSerializer<User>("profiles/users", "users", ((App)Application.Current).ListUsers);
            User u = new User(TB_login.Text, TB_passwd.Password, TB_question.Text, TB_answer.Text);
            SignIn window = new SignIn();
            ((App)Application.Current).ListUsers.Add(u);
            us.PushData();
            us.CreateProfileDirectory(u.Nickname);
            u.PushData();
            var w = Utilities.GetWindowRef("CreateAccountWindow");
            //w.Close();
           
            window.Show();
        }
    }
}
