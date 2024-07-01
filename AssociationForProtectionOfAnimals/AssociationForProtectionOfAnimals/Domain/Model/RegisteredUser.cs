using System;
using System.Collections.Generic;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class RegisteredUser : Person
    {
        public RegisteredUser():base() { }
        public RegisteredUser(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password)
            : base(id, firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, username, password, AccountType.RegisteredUser) { }

        public RegisteredUser(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password)
            : base(firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, username, password, AccountType.RegisteredUser) { }

        public override string[] ToCSV()
        {
            return base.ToCSV();
        }

        public override void FromCSV(string[] values)
        {
            base.FromCSV(values);
        }
    }
}
