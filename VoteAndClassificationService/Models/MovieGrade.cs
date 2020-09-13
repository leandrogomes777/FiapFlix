using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoteAndClassificationService.Models
{
    public class MovieGrade
    {
        [Key]
        public long MovieGradeID
        {
            get;set;
        }

        public long MovieID
        {
            get;set;
        }

        public int Grade
        {
            get;set;
        }
    }
}
