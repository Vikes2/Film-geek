using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        [XmlIgnore]
        public Dictionary<Film,float> Rating{ get; set; }
        [XmlIgnore]
        public Dictionary<Film,bool> WatchStatus { get; set; }

        public User()
        {
            Nickname = "test_user";
            ImagePath = "/resources/Avatars/Default.png";
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray("1234");
        
        }

        public User(string nickname, string password, string securityquestion, string securityanswer)
        {
            ImagePath = "/resources/Avatars/Default.png";
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray(password);
            Nickname = nickname;
            SecurityQuestion = securityquestion;
            SecurityAnswer = securityanswer;
        }
    }
}
