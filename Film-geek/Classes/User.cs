using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Film_geek.Classes
{

    public class User : IDataErrorInfo
    {
        public string Nickname { get; set; }
        public string ImagePath { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        private string password;
        public string Password {
            get
            {
                return password;
            }
            set {
                PasswordEncoder pe = new PasswordEncoder();
                password = pe.EncryptWithByteArray(value);
            }
        }
        public Dictionary<Film, float> Rating { get; set; }
        public Dictionary<Film, bool> WatchStatus { get; set; }

        //Konstruktory
        public User()
        {
            ImagePath = "/resources/Avatars/Default.png";

        }
        #region konstruktor dla testow
        public User(string nick)
        {
            Nickname = nick;
            ImagePath = "/resources/Avatars/Default.png";
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray("1234");
        }
        #endregion
        public User(string nickname, string password, string securityquestion, string securityanswer)
        {
            ImagePath = "/resources/Avatars/Default.png";
            PasswordEncoder pe = new PasswordEncoder();
            Password = pe.EncryptWithByteArray(password);
            Nickname = nickname;
            SecurityQuestion = securityquestion;
            SecurityAnswer = securityanswer;
        }
        //Koniec konstruktorów


        bool IsUserNameOccupied(string username)
        {
            foreach (User u in ((App)Application.Current).ListUsers)
            {
                if (username == u.Nickname)
                {
                    return true;
                }
            }
            return false;
        }

        //Obsługa walidacji
        public string Error { get { return null; } }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Nickname":
                        if (IsUserNameOccupied(this.Nickname))
                            return "Nazwa użytkownika zajęta.";
                        break;
                    case "SecurityQuestion":
                        if (SecurityQuestion == null || SecurityQuestion == String.Empty)
                            return "Pytanie zabezpieczające zbyt krótkie.";
                        break;
                    case "SecurityAnswer":
                        if (SecurityAnswer == null || SecurityAnswer == String.Empty)
                            return "Odpowiedź na pytanie zbyt krótka.";
                        break;
                }

                return string.Empty;
            }
        }
        //Koniec obsługi walidacji
    }
}
