﻿using Film_geek.Classes;
using Film_geek.Classes.Serializer;
using Film_geek.UserControls;
using Film_geek.Util;
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
        public ProfilesView ProfilesView { get; set; }
        public PasswordInputView PasswordView { get; set; }

        public SignIn()
        {
            InitializeComponent();
            ((App)Application.Current).SignIn = this;

            ProfilesView = new ProfilesView();
            PasswordView = new PasswordInputView();
            GD_SignInContent.Children.Add(ProfilesView);

            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Akcja" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Przygoda" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Animacja" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Biografia" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Komedia" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Kryminał" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Dokument" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Dramat" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Familijny" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Fantasy" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Historyczny" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Horror" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Muzyczny" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Tajemniczy" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Romans" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Sci-Fi" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Krótkometrażowy" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Dreszczowiec" });
            ((App)Application.Current).AllGenres.Add(new FilmGenre() { Name = "Western" });

        }

        private void SignInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(MinWidth.ToString());
            //Auth.Instance.LoggedUser = Auth.Instance.users[0];
            //(new Overview()).Show();
            //MessageBox.Show(this.Height.ToString() + "x" + this.Width.ToString());
        }



    }
}
