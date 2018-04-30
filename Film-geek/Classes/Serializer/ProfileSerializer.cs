using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes.Serializer
{
    public class ProfileSerializer<T> : ListSerializer<T>
    {
        private string defaultPath = "profiles/";

        public ProfileSerializer(string fileName, string header, List<T> list) : base(fileName, header, list)
        {
            string DirectoryName = Path.GetDirectoryName(fileName);
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
