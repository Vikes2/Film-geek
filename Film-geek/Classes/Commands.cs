using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Film_geek.Classes
{
    //
    public class FilmCommand
    {
        private static RoutedUICommand add;
        private static RoutedUICommand edit;
        private static RoutedUICommand detail;


        static FilmCommand()
        {
            add = new RoutedUICommand("Add a film", "Add", typeof(FilmCommand));
            edit = new RoutedUICommand("Edit a film", "Edit", typeof(FilmCommand));
            detail = new RoutedUICommand("Film Detail", "Film Detail", typeof(FilmCommand));
            add.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            edit.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            detail.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Control));
        }

        public static RoutedUICommand Add
        {
            get
            {
                return add;
            }
        }

        public static RoutedUICommand Edit
        {
            get
            {
                return edit;
            }
        }

        public static RoutedUICommand Detail
        {
            get
            {
                return detail;
            }
        }

    }

    public class EditFilmCommand
    {
        private static RoutedUICommand edit;

        static EditFilmCommand()
        {
            edit = new RoutedUICommand("Edit a film", "Edit", typeof(EditFilmCommand));
            edit.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
        }

        public static RoutedUICommand Edit
        {
            get
            {
                return edit;
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

    public class ProfileSettings
    {
        private static RoutedUICommand profile;

        static ProfileSettings()
        {
            profile = new RoutedUICommand("Profile edit", "profileSettings", typeof(ProfileSettings));
            profile.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
        }

        public static RoutedUICommand Profile
        {
            get
            {
                return profile;
            }
        }

    }

    public class HelpManual
    {
        private static RoutedUICommand help;

        static HelpManual()
        {
            help = new RoutedUICommand("Help", "Help man", typeof(ProfileSettings));
            help.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
        }

        public static RoutedUICommand Help
        {
            get
            {
                return help;
            }
        }
    }

}
