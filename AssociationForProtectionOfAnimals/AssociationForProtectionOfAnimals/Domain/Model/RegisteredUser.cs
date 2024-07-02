using System;
using System.Collections.Generic;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class RegisteredUser : Person
    {
        protected bool isBlackListed { get; set; }

        public RegisteredUser() : base()
        {
            isBlackListed = false;
        }
        public bool IsBlackListed
        {
            get { return isBlackListed; }
            set { isBlackListed = value; }
        }

        public RegisteredUser(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, Account account)
            : base(id, firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, account)
        {
            this.isBlackListed = false;
        }

        public RegisteredUser(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, Account account)
            : base(firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, account)
        {
            this.isBlackListed = false;
        }

        public override string[] ToCSV()
        {
            var baseData = new List<string>(base.ToCSV())
            {
                isBlackListed.ToString()
            };
            return baseData.ToArray();
        }

        public override void FromCSV(string[] values)
        {
            if (values.Length != 12)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            base.FromCSV(values[0..11]);
            this.isBlackListed = bool.Parse(values[11]);
        }
    }
}
