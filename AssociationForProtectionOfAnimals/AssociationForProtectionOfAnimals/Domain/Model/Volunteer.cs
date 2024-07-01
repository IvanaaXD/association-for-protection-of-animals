using LangLang.Domain.Model.Enums;
using LangLang.Domain.Model;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Volunteer : Person
    {
        public Volunteer() : base() { }
        public Volunteer(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
            : base(id, firstName, lastName, gender, dateOfBirth, phoneNumber, homeAddress, idNumber, username, password, type) { }

        public Volunteer(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
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
