using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDBApp.Models
{
    public class Movie
    {
        public string Name { get; set; }

        public string Year { get; set; }

        public string Plot { get; set; }

        public string Poster { get; set; }

        public string Producer { get; set; }

        public string Actors { get; set; }
    }
}