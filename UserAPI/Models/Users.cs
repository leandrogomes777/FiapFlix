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

        public List<long> WatchedMovies
        {
            get;set;
        }

        public List<long> WatchLaterMovies
        {
            get; set;
        }


    }
}
