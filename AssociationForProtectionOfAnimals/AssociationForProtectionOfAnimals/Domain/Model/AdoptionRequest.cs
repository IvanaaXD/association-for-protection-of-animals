﻿using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class AdoptionRequest : Request
    {
        public DateTime AdoptionDate { get; set; }

        public AdoptionRequest(int registeredUserId, int volunteerId, RequestStatus status, DateTime requestSubmissionDate, DateTime adoptionDate)
            : base(registeredUserId, volunteerId, status, requestSubmissionDate)
        {
            AdoptionDate = adoptionDate;
        }
    }

}
