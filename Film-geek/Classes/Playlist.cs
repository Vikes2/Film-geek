using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Film_geek.Classes
{
    public class Playlist
    {
        public string Name { get; set; }
        [XmlIgnore]
        public ObservableCollection<Film> Films { get; set; }

        public Playlist()
        {
            Films = new ObservableCollection<Film>();
        }
        
        public override string ToString()
        {
            return Name;
        }


    }
}
