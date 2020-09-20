using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{
    public class Users
    {
        [Key]
        public long UserId
        {
            get;set;
        }

        public string Name
        {
            get;
            set;
        }

        public IList<WatchedMovies> WatchedMovies
        {
            get;set;
        }

        public IList<WatchLaterMovies> WatchLaterMovies
        {
            get; set;
        }


    }
}
