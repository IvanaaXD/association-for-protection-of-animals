using AssociationForProtectionOfAnimals.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace AssociationForProtectionOfAnimals.DTO
{
    public class TemporaryShelterRequestDTO : RequestDTO
    {
        public DateTime accommodationDate;
        public DateTime returnDate;
        public DateTime AccommodationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public TemporaryShelterRequest ToTemporaryShelterRequest()
        {
            return new TemporaryShelterRequest(Id, regUser.Id, VolunteerId,RequestStatus,RequestSubmissionDate,AccommodationDate,ReturnDate);
        }
        public TemporaryShelterRequestDTO()
        {
            
        }

        public TemporaryShelterRequestDTO(TemporaryShelterRequest request)
        {
            id = request.Id;
            volunteerId = request.VolunteerId;
            //regUser = //request.RegisteredUserId;
            requestStatus = request.RequestStatus;
            requestSubmissionDate = request.RequestSubmissionDate;
            accommodationDate = request.AccommodationDate;
            returnDate = request.ReturnDate;    
        }
    }
}
