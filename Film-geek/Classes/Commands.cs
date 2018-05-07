using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Film_geek.Classes
{
    //

    public class AddFilmCommand
    {
        private static RoutedUICommand add;

        static AddFilmCommand()
        {
            add = new RoutedUICommand("Add film", "Add", typeof(AddFilmCommand));
            add.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
        }

        public static RoutedUICommand Add
        {
            get
            {
                return add;
            }
        }

    }

    public class LogOutCommand
    {
        private static RoutedUICommand logOut;

        static LogOutCommand()
        {
            logOut = new RoutedUICommand("Log user out", "LogOut", typeof(LogOutCommand));
            logOut.InputGestures.Add(new KeyGesture(Key.L, ModifierKeys.Control));
        }

        public static RoutedUICommand LogOut
        {
            get
            {
                return logOut;
            }
        }

    }

}
