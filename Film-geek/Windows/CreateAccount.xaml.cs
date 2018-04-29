using Film_geek.Classes;
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
    /// Logika interakcji dla klasy create_account.xaml
    /// </summary>
    public partial class CreateAccount : Window
    {
        public CreateAccount()
        {
            InitializeComponent();
            //((App)Application.Current).CreateAccount = this;
        }

        private void B_ok_Click(object sender, RoutedEventArgs e)
        {

            User u = new User(TB_login.Text, TB_passwd.Text, TB_question.Text, TB_answer.Text);
           // SignIn.ListUsers.Add(u);
        }
    }
}
