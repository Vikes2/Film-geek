﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film_geek.Classes
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<FilmGenre> Genres { get; set; }
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<int> Playlists { get; set; }

        public Film()
        {
            ImagePath = "/resources/Images/FilmTest.png";
            Directors = new List<Director>();
            Actors = new List<Actor>();
            Playlists = new List<int>();
            Genres = new List<FilmGenre>();
        }

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
    }

}
