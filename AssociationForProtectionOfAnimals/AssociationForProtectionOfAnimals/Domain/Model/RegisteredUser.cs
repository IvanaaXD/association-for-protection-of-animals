using System;
using System.Collections.Generic;
using LangLang.Domain.Model.Enums;

namespace LangLang.Domain.Model
{
    public class RegisteredUser : Person
    {
        public RegisteredUser() : base() { }
        public RegisteredUser(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
            : base(id, firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, username, password, type) { }

        public RegisteredUser(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
            : base(firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, username, password, type) { }

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
