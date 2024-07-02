using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Request
    {
        public RequestStatus Status { get; set; }
        public DateTime RequestSubmissionDate { get; set; }

        public Request(RequestStatus status, DateTime requestSubmissionDate)
        {
            Status = status;
            RequestSubmissionDate = requestSubmissionDate;
        }
    }
}
