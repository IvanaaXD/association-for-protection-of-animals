using System.Collections.Generic;
using System.Linq;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Storage;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.Repository
{
    public class RegisteredUserRepo : Subject, IRegisteredUserRepo
    {
        private readonly List<RegisteredUser> _users;
        private readonly Storage<RegisteredUser> _storage;

        public RegisteredUserRepo()
        {
            _storage = new Storage<RegisteredUser>("registeredUsers.csv");
            _users = _storage.Load();

        }

        public int GenerateId()
        {
            if (_users.Count == 0) return 1;
            return _users.Last().Id + 1;
        }

        public RegisteredUser AddRegisteredUser(RegisteredUser user)
        {
            user.Id = GenerateId();
            _users.Add(user);
            _storage.Save(_users);
            NotifyObservers();
            return user;
        }

        public RegisteredUser? UpdateRegisteredUser(RegisteredUser user)
        {
            RegisteredUser? oldRegisteredUser = GetRegisteredUserById(user.Id);
            if (oldRegisteredUser == null) return null;

            oldRegisteredUser.FirstName = user.FirstName;
            oldRegisteredUser.LastName = user.LastName;
            oldRegisteredUser.Gender = user.Gender;
            oldRegisteredUser.DateOfBirth = user.DateOfBirth;
            oldRegisteredUser.PhoneNumber = user.PhoneNumber;
            oldRegisteredUser.HomeAddress = user.HomeAddress;
            oldRegisteredUser.IdNumber = user.IdNumber;
            oldRegisteredUser.Username = user.Username;
            oldRegisteredUser.Password = user.Password;
            oldRegisteredUser.Type = user.Type;

            _storage.Save(_users);
            NotifyObservers();
            return oldRegisteredUser;
        }

        public RegisteredUser? RemoveRegisteredUser(int id)
        {
            RegisteredUser? user = GetRegisteredUserById(id);
            if (user == null) return null;

            _users.Remove(user);
            return user;
        }

        public RegisteredUser? GetRegisteredUserById(int id)
        {
            return _users.Find(v => v.Id == id);
        }

        public List<RegisteredUser> GetAllRegisteredUsers()
        {
            return _users;
        }
    }
}
