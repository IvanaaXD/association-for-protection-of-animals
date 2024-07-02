﻿using AssociationForProtectionOfAnimals.Domain.Model.Enums;
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
        protected Place place;
        protected string phoneNumber;
        protected string idNumber;
        protected Account account;

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

        public Place Place
        {
            get { return place; }
            set { place = value; }
        }

        public string IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        public Account Account
        {
            get { return account; }
            set { account = value; }
        }

        protected Person()
        {
            firstName = "";
            lastName = "";
            phoneNumber = "";
            homeAddress = "";
            idNumber = "";
            account = new Account();
            place = new Place();
        }

        protected Person(int id, string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, Place place, string idNumber, Account account)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.homeAddress = homeAddress;
            this.place = place;
            this.idNumber = idNumber;
            this.account = account;
        }

        protected Person(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber, string homeAddress, Place place, string idNumber, Account account)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.homeAddress = homeAddress;
            this.place = place;
            this.idNumber = idNumber;
            this.account = account;
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
                place.Name,
                place.PostalCode.ToString(),
                phoneNumber,
                idNumber,
                account.Username,
                account.Password,
                account.Type.ToString()
            };
        }

        public virtual void FromCSV(string[] values)
        {
            if (values.Length != 13)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            id = int.Parse(values[0]);
            firstName = values[1];
            lastName = values[2];
            gender = (Gender)Enum.Parse(typeof(Gender), values[3]);
            dateOfBirth = DateTime.Parse(values[4]);
            homeAddress = values[5];
            place = new Place(values[6], int.Parse(values[7]));
            phoneNumber = values[8];
            idNumber = values[9];
            account = new Account(values[10], values[11], (AccountType)Enum.Parse(typeof(AccountType), values[12]));
        }
    }
}
