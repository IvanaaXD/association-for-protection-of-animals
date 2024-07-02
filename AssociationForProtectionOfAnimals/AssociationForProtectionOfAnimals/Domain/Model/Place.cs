using AssociationForProtectionOfAnimals.Storage.Serialization;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Place : ISerializable
    {
        protected string name;
        protected int postalCode;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public Place()
        {
            name = "";
            postalCode = 0;
        }

        public Place(string name, int postalCode)
        {
            this.name = name;
            this.postalCode = postalCode;
        }

        public string[] ToCSV()
        {
            return new string[]
            {
                name,
                postalCode.ToString()
            };
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            name = values[0];
            postalCode = int.Parse(values[1]);
        }
    }
}

