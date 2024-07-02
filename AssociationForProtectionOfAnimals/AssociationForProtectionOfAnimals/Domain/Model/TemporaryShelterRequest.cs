using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class TemporaryShelterRequest : Request
    {
        public DateTime AccommodationDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public TemporaryShelterRequest(int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime accommodationDate, DateTime returnDate)
            : base(registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            AccommodationDate = accommodationDate;
            ReturnDate = returnDate;
        }
    }
}
