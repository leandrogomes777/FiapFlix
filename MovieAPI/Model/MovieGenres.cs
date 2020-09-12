using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Model
{
    public class MovieGenres
    {
        [Key]
        [Column(Order=1)]
        public long MovieId
        {
            get;
            set;
        }

        public Movies Movie
        {
            get; set;
        }

        [Key]
        [Column(Order = 2)]
        public long GenreId
        {
            get;
            set;
        }

        public Genres Genre
        {
            get;
            set;
        }
    }
}
