using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes.Serializer
{
    public class FilmSerializer<T> : ListSerializer<T>
    {

        public FilmSerializer(string nickname, string header, ObservableCollection<T> list) : base(Path.Combine("profiles", nickname, "films"), header, list)
        {

        }

    }
}
