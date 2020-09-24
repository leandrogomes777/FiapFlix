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
        public long UsersId
        {
            get;set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<WatchedMovies> WatchedMovies
        {
            get;set;
        }

        public ICollection<WatchLaterMovies> WatchLaterMovies
        {
            get; set;
        }


    }
}
