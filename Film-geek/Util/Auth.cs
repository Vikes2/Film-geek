﻿using Film_geek.Classes;
using Film_geek.Classes.Serializer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Util
{
    public class Auth
    {
        private static Auth singleton = null;
        private ProfileSerializer<User> profileSerializer;
        private PlaylistSerializer<Playlist> playlistSerializer;

        public ObservableCollection<User> users;

        private Auth()
        {
            users = new ObservableCollection<User>();
            profileSerializer = new ProfileSerializer<User>("users", "users", users);
            LoadUsersFromFile();
        }

        private void LoadUsersFromFile()
        {
            users = profileSerializer.PullData();
            foreach(var user in users)
            {
                PlaylistSerializer<Playlist> ps = new PlaylistSerializer<Playlist>(user.Nickname, "playlists", user.Playlists);
                user.Playlists = ps.PullData();
            }
        }

        public void AddNewUser(User user)
        {
            if(user.Nickname.Length > 0)
            {
                users.Add(user);
                profileSerializer.PushData();
                profileSerializer.CreateProfileDirectory(user.Nickname);
                playlistSerializer = new PlaylistSerializer<Playlist>(user.Nickname, "playlists", user.Playlists);
                playlistSerializer.PushData();
            }
        }

        public static Auth Instance
        {
            get
            {
                if (singleton == null)
                    singleton = new Auth();
                return singleton;
            }
        }
    }
}
