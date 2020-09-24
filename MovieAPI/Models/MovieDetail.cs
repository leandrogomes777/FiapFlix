using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Models
{
    public class MovieDetail
    {
        [Key]
        public long MovieDetailId
        {
            get;set;
        }

        public string MovieDescription
        {
            get;set;
        }

        public string PosterImageLink
        {
            get;set;
        }

        public DateTime ReleaseDate
        {
            get;
            set;
        }

        [ForeignKey("MovieId")]
        public long MovieId
        {
            get;set;
        }
        public Movies Movies
        {
            get; set;
        }

    }
}
