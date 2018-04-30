using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Film_geek.Classes.Serializer
{
    public class PlaylistSerializer<T> : ListSerializer<T>
    {

        public PlaylistSerializer(string nickname, string header, List<T> list) : base(Path.Combine("profiles", nickname, "playlists"), header, list)
        {
            
        }

    }
}
