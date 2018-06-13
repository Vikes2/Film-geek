using Film_geek.Classes;
using Film_geek.Classes.Serializer;
using Film_geek.UserControls;
using Film_geek.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Film_geek.Util
{
    public class Auth : INotifyPropertyChanged
    {
        private static Auth singleton = null;
        private ProfileSerializer<User> profileSerializer;
        private PlaylistSerializer<Playlist> playlistSerializer;
        private FilmSerializer<Film> filmSerializer;

        public ObservableCollection<User> users;
        private User loggedUser;
        
        public User LoggedUser
        {
            get { return loggedUser; }
            set { loggedUser = value; ; OnPropertyChanged("LoggedUser"); }

        }
        public List<Playlist> UsersPlaylists { get; set; }


        public static Auth Instance
        {
            get
            {
                if (singleton == null)
                    singleton = new Auth();
                return singleton;
            }
        }

        private Auth()
        {
            users = new ObservableCollection<User>();
            profileSerializer = new ProfileSerializer<User>("users", "users", users);
            CreateDefaultDirectories();
            LoadUsersFromFile();
        }

        private void CreateDefaultDirectories()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Avatars");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Film-geek", "Covers");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void LoadUsersFromFile()
        {
            users = profileSerializer.PullData();
        }

        private void LoadFilms()
        {
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);

            List<Film> films = new List<Film>(LoggedUser.Playlists[0].Films);

            foreach(var film in films)
            {
                foreach (var userPlaylist in LoggedUser.Playlists)
                {
                    if (film.Playlists.Contains(userPlaylist.Id))
                    {
                        AddFilmToPlaylist(film, userPlaylist);
                    }
                }
            }
        }

        public void AddFilmToPlaylist(Film film, Playlist playlist = null)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);

            if (playlist != null)
            {
                if (!playlist.Films.Contains(film))
                {
                    playlist.Films.Add(film);
                    if (!film.Playlists.Contains(playlist.Id))
                    {
                        film.Playlists.Add(playlist.Id);
                    }
                }
            }

            if (!LoggedUser.Playlists[0].Films.Contains(film))
            {
                LoggedUser.Playlists[0].Films.Add(film);
            }

            playlistSerializer.PushData();
            filmSerializer.PushData();
        }

        public void DeleteFilm(Film film)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);


            foreach (Playlist p in LoggedUser.Playlists)
            {
                if (p.Films.Contains(film))
                {
                    p.Films.Remove(film);
                }
            }

            playlistSerializer.PushData();
            filmSerializer.PushData();
        }

        public void SetFilmsIntoPlaylist(ObservableCollection<Film> films, int idPlaylist)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);


            foreach (Playlist playlist in LoggedUser.Playlists)
            {
                if (playlist.Id == idPlaylist)
                {

                    playlist.Films.Clear();
                    foreach (Film film in films)
                    {
                        playlist.Films.Add(film);
                        //======================================================
                        // neeeed serializowac zmiany

                    }
                }
            }
            playlistSerializer.PushData();
            filmSerializer.PushData();
        }

        public void DeletePlaylist(Playlist playlist)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);

            LoggedUser.Playlists.Remove(playlist);

            filmSerializer.PushData();
            playlistSerializer.PushData();
        }

        private int GetPlaylistLastId()
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            LoggedUser.Playlists = playlistSerializer.PullData();

            int id = 0;
            foreach(var playlist in LoggedUser.Playlists)
            {
                id = Math.Max(id, playlist.Id);
            }

            return ++id;
        }

        private int GetFilmLastId()
        {
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);
            LoggedUser.Playlists[0].Films = filmSerializer.PullData();

            int id = 0;
            foreach(var film in LoggedUser.Playlists[0].Films)
            {
                id = Math.Max(id, film.Id);
            }

            return ++id;
        }

        public void AddNewPlaylist(string name)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);

            Playlist newPlaylist = new Playlist()
            {
                Id = GetPlaylistLastId(),
                Name = name
            };

            Overview overview = ((App)Application.Current).Overview;
            overview.PUC.CB_Playlists.ItemsSource = null;
            overview.PUC.CB_Playlists.ItemsSource = LoggedUser.Playlists;
            LoggedUser.AddPlaylist(newPlaylist);
            //LoggedUser.Playlists.Add(newPlaylist);
            playlistSerializer.PushData();
        }

        public void AddNewFilm(Film film)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            LoggedUser.Playlists = playlistSerializer.PullData();

            film.Id = GetFilmLastId();

            foreach (var playlistId in film.Playlists)
            {
                foreach (var playlist in LoggedUser.Playlists)
                {
                    if (playlist.Id == playlistId)
                    {
                        playlist.Films.Add(film);
                    }
                }
            }

            LoggedUser.Playlists[0].Films.Add(film);

            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);
            filmSerializer.PushData();

        }

        public void AddNewUser(User user)
        {
            if(user.Nickname.Length > 0)
            {
                //Create default playlist.
                user.Playlists.Add(
                    new Playlist()
                    {
                        Id = 1,
                        Name = "Wszystko"
                    });
                users.Add(user);

                using (MD5 md5Hash = MD5.Create())
                {
                    user.Id = GetMd5Hash(md5Hash, (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString());
                    user.Password = GetMd5Hash(md5Hash, user.Id + user.Password);
                    user.SecurityAnswer = GetMd5Hash(md5Hash, user.Id + user.SecurityAnswer);
                }

                profileSerializer.PushData();
                profileSerializer.CreateProfileDirectory(user.Id);
                playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
                playlistSerializer.PushData();
                filmSerializer = new FilmSerializer<Film>(user.Id, "films", user.Playlists[0].Films);
                filmSerializer.PushData();
            }
        }

        public void SaveUsers()
        {
            profileSerializer.PushData();
        }

        public void SavePlaylists()
        {
            User user = LoggedUser;
            playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
            playlistSerializer.PushData();
            filmSerializer = new FilmSerializer<Film>(user.Id, "films", user.Playlists[0].Films);
            filmSerializer.PushData();
        }

        public bool CheckSecurityAnswer(User user, string securityAnswer)
        {
            using(MD5 md5Hash = MD5.Create())
            {
                //MessageBox.Show(user.SecurityAnswer);
                //MessageBox.Show(GetMd5Hash(md5Hash, user.Id + securityAnswer));
                return (user.SecurityAnswer == GetMd5Hash(md5Hash, user.Id + securityAnswer));
            }
        }
        
        public void SetPassword(User u, string newPassword)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                u.Password = GetMd5Hash(md5Hash, u.Id + newPassword);
            }
            profileSerializer.PushData();
        }

        public bool LogIn(User user, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                if(GetMd5Hash(md5Hash, user.Id + password) == user.Password)
                {
                    LoggedUser = user;

                    playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
                    LoggedUser.Playlists = playlistSerializer.PullData();
                    filmSerializer = new FilmSerializer<Film>(user.Id, "films", user.Playlists[0].Films);
                    LoggedUser.Playlists[0].Films = filmSerializer.PullData();
                    LoadFilms();
                    return true;
                }
                return false;
            }
        }

        public void LogOut()
        {
            // set nulls 
            LoggedUser = null;
        }

        public string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                new PropertyChangedEventArgs(property));
        }
    }
}
