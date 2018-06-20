using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Film_geek.Classes
{

    public class Film : IDataErrorInfo, INotifyPropertyChanged
    {
        private string imagePath;
        private int rating;

        public int Id { get; set; }
        public string Title { get; set; }
        public List<FilmGenre> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public List<int> Playlists { get; set; }
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImagePath"));
            }
        }
        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rating"));
            }
        }



        public bool isShowing { get; set; }
        public Film()
        {
            ImagePath = "/resources/Images/default_cover.png";
            Genres = new List<FilmGenre>();
            Directors = new List<Director>();
            Actors = new List<Actor>();
            Playlists = new List<int>();
            Genres = new List<FilmGenre>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Title;
        }

        public string DirectorsDetails()
        {
            if (!Directors.Any())
                return "brak reżyserów";
            string directorlist = "";
            foreach (Director d in Directors)
            {
                directorlist += d.ToString()+", ";
            }
            return directorlist.Remove(directorlist.Length - 2);
        }

        public string ActorsDetails()
        {
            if (!Actors.Any())
                return "brak aktorów";
            string actorlist = "";
            foreach (Actor a in Actors)
            {
                actorlist += a.ToString() + ", ";
            }
            return actorlist.Remove(actorlist.Length - 2);
        }

        public string GenresDetails()
        {
            string genrelist = "";
            foreach (FilmGenre g in Genres)
            {
                genrelist += g.ToString() + ", ";
            }
            if (genrelist.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return genrelist.Remove(genrelist.Length - 2);
            }
        }

        public string GenresDesc
        {
            get
            {
                string genrelist = "";
                foreach (FilmGenre g in Genres)
                {
                    genrelist += g.ToString() + ", ";
                }
                if (genrelist.Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return genrelist.Remove(genrelist.Length - 2);
                }
            }
        }

        //Obsługa walidacji
        public string Error { get { return null; } }
        public string this[string columnName]
        { 
            get
            {
                switch (columnName)
                {
                    case "Title":
                        if (Title == String.Empty || Title == null)
                            return "Nie wpisano nazwy filmu";
                        break;
                    case "Genres":
                        if (Genres.Count == 0)
                            return "Nie wybrano żadnego gatunku.";
                        break;
                    //case "Directors":
                    //    if (Directors.Count == 0)
                    //        return "Nie wpisano żadnego reżysera.";
                        //break;
                    case "Actors":
                        if (Actors.Count == 0)
                            return "Nie wpisano żadnego aktora.";
                        break;
                }
                return null;
            }
        }
        //Koniec obsługi walidacji
    }

}