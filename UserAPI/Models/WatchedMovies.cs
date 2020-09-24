using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{

    public class WatchedMovies
    {
        public long WatchedMoviesId
        {
            get; set;
        }
        public long MovieId
        {
            get; set;
        }

        public long UsersId
        {
            get; set;
        }

        public Users Users
        {
            get;
            set;
        }

    }
}
