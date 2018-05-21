using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class FilmGenre
    {
        public string Name { get; set; }
        public FilmGenre()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
