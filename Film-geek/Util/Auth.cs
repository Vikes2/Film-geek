using Film_geek.Classes;
using Film_geek.Classes.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Util
{
    public class Auth
    {
        private static Auth singleton = null;
        private ProfileSerializer<User> profileSerializer;
        private PlaylistSerializer<Playlist> playlistSerializer;
        private FilmSerializer<Film> filmSerializer;

        public ObservableCollection<User> users;
        public User LoggedUser { get; set; }

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

        private void LoadUsersFromFile()
        {
            users = profileSerializer.PullData();
        }

        private void LoadFilms()
        {
            ObservableCollection<Film> films = new ObservableCollection<Film>();
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);
            films = filmSerializer.PullData();

            foreach(Film film in films)
            {
                foreach(Playlist playlist in film.Playlists)
                {
                    foreach (Playlist userPlaylist in LoggedUser.Playlists)
                    {
                        if (userPlaylist.Name == playlist.Name)
                        {
                            AddFilmToPlaylist(userPlaylist, film);
                            break;
                        }
                    }
                }
            }

        }

        private void AddFilmToPlaylist(Playlist playlist, Film film)
        {
            playlist.Films.Add(film);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", playlist.Films);
            filmSerializer.PushData();
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
        }

        public void AddNewUser(User user)
        {
            if(user.Nickname.Length > 0)
            {
                users.Add(user);
                using (MD5 md5Hash = MD5.Create())
                {
                    user.Id = GetMd5Hash(md5Hash, (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString());
                }
                profileSerializer.PushData();
                profileSerializer.CreateProfileDirectory(user.Id);
                playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
                playlistSerializer.PushData();
                filmSerializer = new FilmSerializer<Film>(user.Id, "films", user.Playlists[0].Films);
                filmSerializer.PushData();
            }
        }

        public void LogIn(User user)
        {
            LoggedUser = user;
            playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
            LoggedUser.Playlists = playlistSerializer.PullData();
            LoadFilms();
        }

        public void LogOut()
        {
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

    }
}
