using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IRepository;
using AssociationForProtectionOfAnimals.Storage;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class VolunteerController
    {
        private readonly IVolunteerRepo _volunteers;
        private readonly IRegisteredUserRepo _users;
        private readonly IAdminRepo _admin;
        private readonly IAnimalRepo _animals;

        public VolunteerController()
        {
            _volunteers = Injector.CreateInstance<IVolunteerRepo>();
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
            _admin = Injector.CreateInstance<IAdminRepo>();
            _animals = Injector.CreateInstance<IAnimalRepo>();
        }

        public RegisteredUser? GetById(int id)
        {
            return _volunteers.GetById(id);
        }
        public List<RegisteredUser> GetAllRegisteredUsers()
        {
            return _volunteers.GetAllRegisteredUsers();
        }
        public RegisteredUser AddUser(RegisteredUser user)
        {
            return _volunteers.AddUser(user);
        }

        public RegisteredUser? UpdateUser(RegisteredUser? user)
        {
            return _volunteers.UpdateUser(user);
        }

        public RegisteredUser? RemoveUser(int id)
        {
            RegisteredUser? user = GetById(id);
            if (user == null) return null;
            _volunteers.RemoveUser(id);
            return user;
        }

        public Animal AddAnimal(Animal animal)
        {
            return _animals.AddAnimal(animal);
        }

        public Animal? UpdateAnimal(Animal animal)
        {
            return _animals.UpdateAnimal(animal);
        }

        public Animal? RemoveAnimal(int id)
        {
            return _animals.RemoveAnimal(id);
        }

        public Animal? GetAnimalById(int id)
        {
            return _animals.GetAnimalById(id);
        }

        public List<Animal> GetAllAnimals()
        {
            return _animals.GetAllAnimals();
        }


        public void Subscribe(IObserver observer)
        {
            _volunteers.Subscribe(observer);
            _users.Subscribe(observer);
        }
    }
}