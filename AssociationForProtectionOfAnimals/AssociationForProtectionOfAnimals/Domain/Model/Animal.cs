using AssociationForProtectionOfAnimals.Storage.Serialization;
using System;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Animal : ISerializable
    {
        protected int id;
        protected string name;
        protected int age;
        protected double weight;
        protected double height;
        protected string description;
        protected string address;
        protected string medicalStatus;
        protected Breed breed;
        protected Species species;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string MedicalStatus
        {
            get { return medicalStatus; }
            set { medicalStatus = value; }
        }

        public Breed Breed
        {
            get { return breed; }
            set { breed = value; }
        }

        public Species Species
        {
            get { return species; }
            set { species = value; }
        }

        public Animal()
        {
            name = "";
            description = "";
            address = "";
            medicalStatus = "";
            breed = new Breed();
            species = new Species();
        }

        public Animal(int id, string name, int age, double weight, double height, string description, string address, string medicalStatus, Breed breed, Species species)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.weight = weight;
            this.height = height;
            this.description = description;
            this.address = address;
            this.medicalStatus = medicalStatus;
            this.breed = breed;
            this.species = species;
        }

        public override string ToString()
        {
            return $"{name}, {age} years old";
        }

        public virtual string[] ToCSV()
        {
            return new string[]
            {
                id.ToString(),
                name,
                age.ToString(),
                weight.ToString(),
                height.ToString(),
                description,
                address,
                medicalStatus,
                breed.Name,
                breed.Description,
                species.Name,
                species.Description
            };
        }

        public virtual void FromCSV(string[] values)
        {
            if (values.Length != 12)
            {
                throw new ArgumentException("Invalid number of values for CSV deserialization.");
            }

            id = int.Parse(values[0]);
            name = values[1];
            age = int.Parse(values[2]);
            weight = double.Parse(values[3]);
            height = double.Parse(values[4]);
            description = values[5];
            address = values[6];
            medicalStatus = values[7];
            breed = new Breed(values[8], values[9]);
            species = new Species(values[10], values[11]);
        }
    }
}
