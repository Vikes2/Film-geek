using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Film> Films { get; set; }

        public Playlist()
        {
            Films = new List<Film>();
        }
        
        public override string ToString()
        {
            return Name;
        }


    }
}
