using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class AdoptionRequest : Request
    {
        public DateTime adoptionDate;
        public DateTime AdoptionDate { get; set; }

        public AdoptionRequest(int id,int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime adoptionDate)
            : base(id, registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            this.adoptionDate = adoptionDate;
        }
        public AdoptionRequest(int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime adoptionDate)
            : base(registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            this.adoptionDate = adoptionDate;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                volunteerId.ToString(),
                registeredUserId.ToString(),
                requestStatus.ToString(),
                requestSubmissionDate.ToString("yyyy-MM-dd"),
                adoptionDate.ToString("yyyy-MM-dd")
            };
            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            int id = int.Parse(csvValues[0]);
            int volunteerId = int.Parse(csvValues[1]);
            int registeredUserId = int.Parse(csvValues[2]);
            RequestStatus requestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), csvValues[3]);
            DateTime requestSubmissionDate = DateTime.ParseExact(csvValues[4], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime adoptionDate = DateTime.ParseExact(csvValues[5], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }

}
