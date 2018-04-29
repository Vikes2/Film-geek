using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{

    public class User
    {
        //to do
        public string Nickname { get; set; }
        public string ImagePath { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public List<Playlist> Playlists { get; set; }
        public Dictionary<Film,float> Rating{ get; set; }
        public Dictionary<Film,bool> WatchStatus { get; set; }

        public User()
        {
            Playlists = new List<Playlist>();

            // Avatar
            ImagePath = "/resources/Avatars/Default.png";
            // Hasło
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray("1234");
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







        }
    }
}
