using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{

    public class Film
    {
        public string Title { get; set; }
        public List<FilmGenre> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<Playlist> Playlists { get; }

        public Film()
        {
            ImagePath = "/resources/Images/FilmTest.png";
            Playlists = new List<Playlist>();
        }

        public override string ToString()
        {
            return Title;
        }


    }

}
