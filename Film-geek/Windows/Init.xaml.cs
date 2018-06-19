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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Film_geek.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy Init.xaml
    /// </summary>
    public partial class Init : Window
    {
        public Init()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 1;
            anim.To = 0;
            anim.Duration = TimeSpan.FromSeconds(0.8);
            anim.Completed += HideInitWindow;
            this.BeginAnimation(Window.OpacityProperty, anim);
        }

        private void HideInitWindow(object sender, EventArgs e)
        {
            Hide();
            SignIn signInWindow = new SignIn();
            signInWindow.Opacity = 0;
            signInWindow.Show();
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 1;
            anim.Duration = TimeSpan.FromSeconds(0.6);
            signInWindow.BeginAnimation(Window.OpacityProperty, anim);
        }
    }
}
