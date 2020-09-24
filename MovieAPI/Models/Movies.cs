using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Model
{
    public class Movies
    {
        [Key]
        public long MovieId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime ReleaseDate
        {
            get;
            set;
        }

        public ICollection<MovieGenres> MoviesGenres
        {
            get;
            set;
        }

        public MovieDetail MovieDetail
        {
            get; set;
        }

    }
}
