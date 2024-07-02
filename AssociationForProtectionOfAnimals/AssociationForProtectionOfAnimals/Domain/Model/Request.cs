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
        protected int id;
        protected int volunteerId;
        protected int registeredUserId;
        protected RequestStatus requestStatus;
        protected DateTime requestSubmissionDate;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int VolunteerId
        {
            get { return volunteerId; }
            set { volunteerId = value; }
        }
        
        public int RegisteredUserId
        {
            get { return registeredUserId; }
            set { registeredUserId = value; }
        }
        public DateTime RequestSubmissionDate
        {
            get { return requestSubmissionDate; }
            set { requestSubmissionDate = value; }
        }

        public RequestStatus RequestStatus
        {
            get { return requestStatus; }
            set { requestStatus = value; }
        }
        public Request(int registeredUserId, int volunteerId, RequestStatus requestStatus, DateTime requestSubmissionDate)
        {
            this.registeredUserId = registeredUserId;
            this.volunteerId = volunteerId;
            this.requestStatus = requestStatus;
            this.requestSubmissionDate = requestSubmissionDate;
        }
        public Request(int id, int registeredUserId, int volunteerId, RequestStatus requestStatus, DateTime requestSubmissionDate)
        {
            this.id = id;
            this.registeredUserId = registeredUserId;
            this.volunteerId = volunteerId;
            this.requestStatus = requestStatus;
            this.requestSubmissionDate = requestSubmissionDate;
        }
    }
}
