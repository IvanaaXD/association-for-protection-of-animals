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
        Controller.RegisteredUserController userController = Injector.CreateInstance<Controller.RegisteredUserController>();

        public DateTime adoptionDate;
        public int registeredUserId;
        public DateTime AdoptionDate { get; set; }
        public int RegisteredUserId { get; set; }
        public AdoptionRequestDTO() { }

        public AdoptionRequest ToAdoptionRequest()
        {
            return new AdoptionRequest(Id, RegisteredUserId, VolunteerId, RequestStatus, RequestSubmissionDate, AdoptionDate);
        }
        public AdoptionRequestDTO(AdoptionRequest request)
        {
            id = request.Id;
            volunteerId = request.VolunteerId;
            requestStatus = request.RequestStatus;
            requestSubmissionDate = request.RequestSubmissionDate;
            adoptionDate = request.adoptionDate;

            registeredUserId = request.RegisteredUserId;
            Account account = userController.GetAccountById(registeredUserId);
            username = account.Username;
        }
    }
}
