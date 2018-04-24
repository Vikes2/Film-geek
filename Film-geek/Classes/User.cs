using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class User
    {
        public string Nickname { get; set; }
        public string ImagePath { get; set; }
        public User()
        {
            ImagePath = "/resources/Avatars/Default.png";
        
        }
    }
}
