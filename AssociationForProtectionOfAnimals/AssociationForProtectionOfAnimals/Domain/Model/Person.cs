using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using AssociationForProtectionOfAnimals.Storage.Serialization;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public abstract class Person : ISerializable
    {
        protected int id;
        protected string firstName;
        protected string lastName;
        protected Gender gender;
        protected DateTime dateOfBirth;
        protected string homeAddress;
        protected string phoneNumber;
        protected string idNumber;
        protected string username;
        protected string password;
        protected AccountType type;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string HomeAddress
        {
            get { return homeAddress; }
            set { homeAddress = value; }
        }

        public string IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public AccountType Type
        {
            get { return type; }
            set { type = value; }
        }


        protected Person() 
        {
            firstName = "";
            lastName = "";
            phoneNumber = "";
            homeAddress = "";
            idNumber = "";
        }
        protected Person(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.homeAddress = homeAddress;
            this.idNumber = idNumber;
            this.username = username;
            this.password = password;
            this.type = type;
        }
        protected Person(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, string idNumber, string username, string password, AccountType type)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.homeAddress = homeAddress;
            this.idNumber = idNumber;
            this.username = username;
            this.password = password;
            this.type = type;
        }

        public override string ToString()
        {
            return $"{firstName} {lastName}";
        }
        public virtual string[] ToCSV()
        {
            return new string[]
            {
            id.ToString(),
            firstName,
            lastName,
            gender.ToString(),
            dateOfBirth.ToString("yyyy-MM-dd"),
            homeAddress,
            phoneNumber,
            idNumber,
            username,
            password,
            type.ToString()
            };
        }

        public virtual void FromCSV(string[] values)
        {
            if (values.Length != 11)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            id = int.Parse(values[0]);
            firstName = values[1];
            lastName = values[2];
            gender = (Gender)Enum.Parse(typeof(Gender), values[3]);
            dateOfBirth = DateTime.Parse(values[4]);
            homeAddress = values[5];
            phoneNumber = values[6];
            idNumber = values[7];
            username = values[8];
            password = values[9];
            type = (AccountType)Enum.Parse(typeof(AccountType), values[10]);
        }
    }
}
