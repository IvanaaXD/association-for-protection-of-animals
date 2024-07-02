using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountType Type { get; set; }

        public Account() { }

        public Account(string username, string password, AccountType type)
        {
            Username = username;
            Password = password;
            Type = type;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                Username,
                Password,
                Type.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 3)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            Username = values[0];
            Password = values[1];
            Type = (AccountType)Enum.Parse(typeof(AccountType), values[2]);
        }
    }
}

