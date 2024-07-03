using AssociationForProtectionOfAnimals.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.DTO
{
    public class AdoptionRequestDTO : RequestDTO
    {
        public DateTime adoptionDate;
        public DateTime AdoptionDate { get; set; }
        public AdoptionRequestDTO() { }

        public AdoptionRequest ToAdoptionRequest()
        {
            return new AdoptionRequest(Id, regUser.Id, VolunteerId, RequestStatus, RequestSubmissionDate, AdoptionDate);
        }
        public AdoptionRequestDTO(AdoptionRequest request)
        {
            id = request.Id;
            volunteerId = request.VolunteerId;
            //regUser = //request.RegisteredUserId;
            requestStatus = request.RequestStatus;
            requestSubmissionDate = request.RequestSubmissionDate;
            adoptionDate = request.adoptionDate;
        }
    }
}
