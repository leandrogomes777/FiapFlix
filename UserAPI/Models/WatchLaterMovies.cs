using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{
    public class WatchLaterMovies
    {
        public long Id
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
