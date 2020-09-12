using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCServiceReceive.Models
{
    public class TechnicalProblems
    {
        [Key]
        public long TechnicalProblemsId
        {
            get;set;
        }

        public string Description
        {
            get;set;
        }
    }
}
