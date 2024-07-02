using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using AssociationForProtectionOfAnimals.Storage.Serialization;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Account : ISerializable
    {
        protected int id;
        protected string username;
        protected string password;
        protected AccountType type;

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public Account() { }

        public Account(int id, string username, string password, AccountType type)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.type = type;
        }
        public Account(string username, string password, AccountType type)
        {
            this.username = username;
            this.password = password;
            this.type = type;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                id.ToString(),
                username,
                password,
                type.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 4)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            id = int.Parse(values[0]);
            username = values[1];
            password = values[2];
            type = (AccountType)Enum.Parse(typeof(AccountType), values[3]);
        }
    }
}
