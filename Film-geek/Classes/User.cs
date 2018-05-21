using Film_geek.Classes.Serializer;
using Film_geek.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace Film_geek.Classes
{

    public class User : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string id;
        private string imagePath;
        private string password;
        private ObservableCollection<Playlist> playlists;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Nickname { get; set; }
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged("");
            }
        }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string Password {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        [XmlIgnore]
        public ObservableCollection<Playlist> Playlists
        {
            get
            {
                return playlists;
            }
            set
            {
                playlists = value;
                OnPropertyChanged("");
            }
        }
        [XmlIgnore]
        public Dictionary<Film,float> Rating{ get; set; }
        [XmlIgnore]
        public Dictionary<Film,bool> WatchStatus { get; set; }

        public User()
        {
            Playlists = new ObservableCollection<Playlist>();
            Rating = new Dictionary<Film, float>();
            WatchStatus = new Dictionary<Film, bool>();
            ImagePath = "/resources/Avatars/Default.png";
        }

        public void AddPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist);
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
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
                        if (Nickname == String.Empty || Nickname == null)
                            return "Nazwa użytkownika nie może być pusta.";
                        break;
                    case "SecurityQuestion":
                        if (SecurityQuestion == null || SecurityQuestion == String.Empty)
                            return "Pytanie zabezpieczające zbyt krótkie.";
                        break;
                    case "SecurityAnswer":
                        if (SecurityAnswer == null || SecurityAnswer == String.Empty)
                            return "Odpowiedź na pytanie zbyt krótka.";
                        break;
                }

                return null;
            }
        }
        //Koniec obsługi walidacji
    }
}
