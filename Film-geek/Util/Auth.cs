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
using System.Windows;

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
                    film.Playlists.Add(playlist.Id);
                }
            }

            if (!LoggedUser.Playlists[0].Films.Contains(film))
            {
                LoggedUser.Playlists[0].Films.Add(film);
            }

            filmSerializer.PushData();
            playlistSerializer.PushData();
        }

        public void DeleteFilm(Film film)
        {
            playlistSerializer = new PlaylistSerializer<Playlist>(LoggedUser.Id, "playlists", LoggedUser.Playlists);
            filmSerializer = new FilmSerializer<Film>(LoggedUser.Id, "films", LoggedUser.Playlists[0].Films);

            ObservableCollection<Playlist> playlists = new ObservableCollection<Playlist>();
            playlists = LoggedUser.Playlists;
            foreach (Playlist p in playlists)
            {
                if (p.Films.Contains(film))
                {
                    p.Films.Remove(film);
                }
            }

            filmSerializer.PushData();
            playlistSerializer.PushData();
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
                    user.SecurityAnswer = GetMd5Hash(md5Hash, user.Id + user.Password);
                }

                profileSerializer.PushData();
                profileSerializer.CreateProfileDirectory(user.Id);
                playlistSerializer = new PlaylistSerializer<Playlist>(user.Id, "playlists", user.Playlists);
                playlistSerializer.PushData();
                filmSerializer = new FilmSerializer<Film>(user.Id, "films", user.Playlists[0].Films);
                filmSerializer.PushData();
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

    }
}
