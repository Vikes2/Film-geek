using Film_geek.Classes.Serializer;
using Film_geek.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Film_geek.Classes
{

    public class User : IDataErrorInfo
    {
        //to do
        private PlaylistSerializer<Playlist> ps;
        
        public string Nickname { get; set; }
        public string ImagePath { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        private string password;
        public string Password {
            get
            {
                return password;
            }
            set {
                PasswordEncoder pe = new PasswordEncoder();
                password = pe.EncryptWithByteArray(value);
            }
        }
        [XmlIgnore]
        public ObservableCollection<Playlist> Playlists { get; set; }
        [XmlIgnore]
        public Dictionary<Film,float> Rating{ get; set; }
        [XmlIgnore]
        public Dictionary<Film,bool> WatchStatus { get; set; }
        
        public User()
        {
            Playlists = new ObservableCollection<Playlist>();
            ImagePath = "/resources/Avatars/Default.png";
            // Hasło
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray("1234");

            #region playlists
            // listy
            Playlist pl = new Playlist();
            pl.Name = "miłe panie";

            Film f = new Film();
            f.Title = "Hot Girls Wanted";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "American Pie";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "Fifty Shades of Grey";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "Fifty Shades Freed";
            pl.Films.Add(f);

            Playlists.Add(pl);

            pl = new Playlist();
            pl.Name = "serialowe";

            f = new Film();
            f.Title = "Hannibal";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "Dexter";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "West World";
            pl.Films.Add(f);
            f = new Film();
            f.Title = "Suits";
            pl.Films.Add(f);
            Playlists.Add(pl); 
            #endregion
        }

        // We don't use this anymore.
       
        //public User(string nickname, string password, string securityquestion, string securityanswer)
        //{
        //    ImagePath = "/resources/Avatars/Default.png";
        //    PasswordEncoder pe = new PasswordEncoder();
        //    Password = pe.EncryptWithByteArray(password);
        //    Nickname = nickname;
        //    SecurityQuestion = securityquestion;
        //    SecurityAnswer = securityanswer;

        //    Playlists = new ObservableCollection<Playlist>();

        //    #region playlists
        //    // listy
        //    Playlist pl = new Playlist();
        //    pl.Name = "miłe panie";

        //    Film f = new Film();
        //    f.Title = "Hot Girls Wanted";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "American Pie";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "Fifty Shades of Grey";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "Fifty Shades Freed";
        //    pl.Films.Add(f);

        //    Playlists.Add(pl);

        //    pl = new Playlist();
        //    pl.Name = "serialowe";

        //    f = new Film();
        //    f.Title = "Hannibal";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "Dexter";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "West World";
        //    pl.Films.Add(f);
        //    f = new Film();
        //    f.Title = "Suits";
        //    pl.Films.Add(f);
        //    Playlists.Add(pl); 
        //    #endregion
        //}

        public void PushData()
        {
            ps = new PlaylistSerializer<Playlist>(Nickname, "playlists", Playlists);
            ps.PushData();
        }
        //Koniec konstruktorów


        bool IsUserNameOccupied(string username)
        {
            foreach (User u in (Auth.Instance.users))
            {
                if (username == u.Nickname)
                {
                    return true;
                }
            }
            return false;
        }

        //Obsługa walidacji
        public string Error { get { return null; } }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Nickname":
                        if (IsUserNameOccupied(this.Nickname))
                            return "Nazwa użytkownika zajęta.";
                        if (this.Nickname == String.Empty || this.Nickname == null)
                            return "Nazwa użytkownika nie może być pusta.";
                        break;;
                    case "SecurityQuestion":
                        if (SecurityQuestion == null || SecurityQuestion == String.Empty)
                            return "Pytanie zabezpieczające zbyt krótkie.";
                        break;
                    case "SecurityAnswer":
                        if (SecurityAnswer == null || SecurityAnswer == String.Empty)
                            return "Odpowiedź na pytanie zbyt krótka.";
                        break;
                }

                return string.Empty;
            }
        }
        //Koniec obsługi walidacji
    }
}
