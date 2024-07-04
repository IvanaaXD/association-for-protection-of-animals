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
        Controller.RegisteredUserController userController = Injector.CreateInstance<Controller.RegisteredUserController>();
        
        public DateTime accommodationDate;
        public DateTime returnDate;
        public int registeredUserId;
        public int RegisteredUserId { get; set; }
        public DateTime AccommodationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public TemporaryShelterRequest ToTemporaryShelterRequest()
        {
            return new TemporaryShelterRequest(Id, RegisteredUserId, VolunteerId,RequestStatus,RequestSubmissionDate,AccommodationDate,ReturnDate);
        }
        public TemporaryShelterRequestDTO()
        {
            
        }

        public TemporaryShelterRequestDTO(TemporaryShelterRequest request)
        {
            id = request.Id;
            volunteerId = request.VolunteerId;
            
            requestStatus = request.RequestStatus;
            requestSubmissionDate = request.RequestSubmissionDate;
            accommodationDate = request.AccommodationDate;
            returnDate = request.ReturnDate;

            registeredUserId = request.RegisteredUserId;
            Account account = userController.GetAccountById(registeredUserId);
            username = account.Username;    
        }
    }
}
