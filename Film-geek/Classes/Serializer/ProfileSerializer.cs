using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes.Serializer
{
    public class ProfileSerializer<T> : ListSerializer<T>
    {
        private string defaultPath = "profiles/";

        public ProfileSerializer(string fileName, string header, ObservableCollection<T> list) : base(Path.Combine("profiles", fileName), header, list)
        {
            string DirectoryName = Path.GetDirectoryName(Path.Combine(defaultPath, fileName));
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }
        }

        public void CreateProfileDirectory(string name)
        {
            string path = Path.Combine(defaultPath, name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
