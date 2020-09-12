using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSCServiceReceive.Models
{
    public class ServiceRequest
    {
        [Key]
        public long ServiceRequestId
        {
            get; set;
        }

        public TechnicalProblems TechnicalProblem
        {
            get; set;
        }
        public string Message
        {
            get;set;
        }
    }
}
