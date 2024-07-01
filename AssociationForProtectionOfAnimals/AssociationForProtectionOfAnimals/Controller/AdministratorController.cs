using AssociationForProtectionOfAnimals.Observer;
using System;
using System.Collections.Generic;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System.Linq;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class AdministratorController
    {
        private readonly IAdminRepo _admins;
        private readonly IVolunteerRepo? _volunteers;
        private readonly IRegisteredUserRepo? _users;

        public AdministratorController()
        {
            _admins = Injector.CreateInstance<IAdminRepo>();
            _volunteers = Injector.CreateInstance<IVolunteerRepo>();
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
        }

        public Administrator? GetAdministrator()
        {
            return _admins.GetAdmin();
        }

        public Volunteer? GetById(int volunteerId)
        {
            return _admins.GetById(volunteerId);
        }

        public List<Volunteer> GetAllVolunteers()
        {
            return _admins.GetAll();
        }

        public Volunteer Add(Volunteer volunteer)
        {
            return _admins.Add(volunteer);
        }

        public Volunteer Update(Volunteer volunteer)
        {
            return _admins.Update(volunteer);
        }

        public void UpdateAdministrator(Administrator director)
        {
            _admins.UpdateAdministrator(director);
        }

        public void Delete(Volunteer volunteer)
        {
            _admins.Remove(volunteer.Id);
        }

        public void Subscribe(IObserver observer)
        {
            _admins.Subscribe(observer);
            _volunteers.Subscribe(observer);
        }

        /*public List<Volunteer> FindVolunteersByCriteria(Language language, LanguageLevel levelOfLanguage, DateTime startedWork)
        {
            List<Volunteer> volunteers = GetAllVolunteers();

            var filteredVolunteers = volunteers.Where(volunteer =>
                (language == Language.NULL || (volunteer.Languages != null && volunteer.Languages.Contains(language))) &&
                (levelOfLanguage == LanguageLevel.NULL || (volunteer.LevelOfLanguages != null && volunteer.LevelOfLanguages.Contains(levelOfLanguage))) &&
                (startedWork == DateTime.MinValue || (volunteer.StartedWork.Date >= startedWork.Date))
            ).ToList();

            return filteredVolunteers;
        }*/

        public List<Volunteer> GetAllVolunteers(int page, int pageSize, string sortCriteria, List<Volunteer> volunteers)
        {
            return _admins.GetAllVolunteers(page, pageSize, sortCriteria, volunteers);
        }
        /*public List<Volunteer> GetAllVolunteers(int page, int pageSize, ISortStrategy sortStrategy, List<Volunteer> volunteers)
        {
            return _admins.GetAllVolunteers(page, pageSize, sortStrategy, volunteers);
        }*/

        public Volunteer? GetVolunteerByUsername(string username)
        {
            foreach (Volunteer volunteer in GetAllVolunteers())
                if (volunteer.Username == username)
                    return volunteer;
            return null;
        }
    }
}
