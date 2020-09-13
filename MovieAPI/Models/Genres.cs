using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Model
{
    public class Genres
    {
        [Key]
        public long GenreId
        {
            get;
            set;
        }

        public string Genre
        {
            get;
            set;
        }

        public ICollection<MovieGenres> MoviesGenres
        {
            get;
            set;
        }


    }
}
