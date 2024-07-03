﻿using System;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using AssociationForProtectionOfAnimals.Storage.Serialization;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public class Post : ISerializable
    {
        private int id;
        private DateTime dateOfPosting;
        private DateTime dateOfUpdating;
        private PostStatus postStatus;
        private bool hasCurrentAdopter;
        private int animalId;
        private string person;
        private string adopter;

        private List<int> personLikedIds;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DateOfPosting
        {
            get { return dateOfPosting; }
            set { dateOfPosting = value; }
        }

        public DateTime DateOfUpdating
        {
            get { return dateOfUpdating; }
            set { dateOfUpdating = value; }
        }

        public PostStatus PostStatus
        {
            get { return postStatus; }
            set { postStatus = value; }
        }

        public bool HasCurrentAdopter
        {
            get { return hasCurrentAdopter; }
            set { hasCurrentAdopter = value; }
        }

        public int AnimalId
        {
            get { return animalId; }
            set { animalId = value; }
        }

        public string Person
        {
            get { return person; }
            set { person = value; }
        }

        public string Adopter
        {
            get { return adopter; }
            set { adopter = value; }
        }

        public List<int> PersonLikedIds
        {
            get { return personLikedIds; }
            set { personLikedIds = value; }
        }

        public Post() { }

        public Post(DateTime dateOfPosting, DateTime dateOfUpdating, PostStatus postStatus, bool hasCurrentAdopter, int animalId, string person, string adopter)
        {
            this.dateOfPosting = dateOfPosting;
            this.dateOfUpdating = dateOfUpdating;
            this.postStatus = postStatus;
            this.hasCurrentAdopter = hasCurrentAdopter;
            this.animalId = animalId;
            this.person = person;
            this.adopter = adopter;
        }

        public string[] ToCSV()
        {
            string personLikedIdsStr = string.Join(",", personLikedIds);

            string[] csvValues =
            {
                Id.ToString(),
                DateOfPosting.ToString("yyyy-MM-dd"),
                DateOfUpdating.ToString("yyyy-MM-dd"),
                PostStatus.ToString(),
                HasCurrentAdopter.ToString(),
                AnimalId.ToString(),
                Person.ToString(),
                Adopter.ToString(),
                personLikedIdsStr
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length != 9)
                throw new ArgumentException("Invalid number of post values in CSV");

            id = int.Parse(values[0]);
            dateOfPosting = DateTime.ParseExact(values[1], "yyyy-MM-dd", null);
            dateOfUpdating = DateTime.ParseExact(values[2], "yyyy-MM-dd", null);
            postStatus = (PostStatus)Enum.Parse(typeof(PostStatus), values[3]);
            hasCurrentAdopter = bool.Parse(values[4]);
            animalId = int.Parse(values[5]); 
            person = values[6];
            adopter = values[7];
            personLikedIds = ListFromCSV(values[8]);
        }

        private List<int> ListFromCSV(string listElements)
        {
            List<int> list = new List<int>();
            if (!string.IsNullOrEmpty(listElements))
                list = new List<int>(Array.ConvertAll(listElements.Split(','), int.Parse));

            return list;
        }
    }
}
