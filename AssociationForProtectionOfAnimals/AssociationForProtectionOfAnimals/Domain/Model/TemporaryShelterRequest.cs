using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class TemporaryShelterRequest : Request
    {
        public DateTime accommodationDate;
        public DateTime returnDate;
        public DateTime AccommodationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public TemporaryShelterRequest(int id,int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime accommodationDate, DateTime returnDate)
            : base(id, registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            this.accommodationDate = accommodationDate;
            this.returnDate = returnDate;
        }
        public TemporaryShelterRequest(int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime accommodationDate, DateTime returnDate)
            : base(registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            AccommodationDate = accommodationDate;
            ReturnDate = returnDate;
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
                AccommodationDate.ToString("yyyy-MM-dd"),
                ReturnDate.ToString("yyyy-MM-dd")
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
            DateTime accommodationDate = DateTime.ParseExact(csvValues[5], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime returnDate = DateTime.ParseExact(csvValues[6], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
