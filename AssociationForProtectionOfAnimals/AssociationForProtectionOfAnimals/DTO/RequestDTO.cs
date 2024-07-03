﻿using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AssociationForProtectionOfAnimals.DTO
{
    public class RequestDTO
    {
        protected int id;
        protected int volunteerId;
        protected RegisteredUser regUser;
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

        public RegisteredUser RegUser
        {
            get { return regUser; }
            set { regUser = value; }
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
     
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                // Add any property-specific validation logic here
                switch (columnName)
                {
                    // Example validation
                    // case "AnimalName":
                    //     if (string.IsNullOrWhiteSpace(animalName))
                    //         return "Animal name cannot be empty";
                    //     break;
                    default:
                        return null;
                }
            }
        }

        public Request ToRequest()
        {
            return new Request
            {
                Id = id,
               
            };
        }

        public RequestDTO() { }

        public RequestDTO(Request request)
        {
            id = request.Id;    
            volunteerId = request.VolunteerId;
            //regUser = //request.RegisteredUserId;
            requestStatus = request.RequestStatus;
            requestSubmissionDate= request.RequestSubmissionDate;
        
        }
    }

}
