using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class RegisteredUserController : Subject
    {
        private readonly IRegisteredUserRepo _users;
        private readonly IAnimalRepo _animals;

        public RegisteredUserController()
        {
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
        }

        public void Add(RegisteredUser user)
        {
            /* SEND REQUEST FOR REGISTRATION */
            _users.AddRegisteredUser(user);
        }
        public void Delete(int userId)
        {
            _users.RemoveRegisteredUser(userId);
            NotifyObservers();
        }
        public void Update(RegisteredUser user)
        {
            /* SEND REQUEST FOR ACCOUNT UPDATE */
            _users.UpdateRegisteredUser(user);
        }
        public void Subscribe(IObserver observer)
        {
            _users.Subscribe(observer);
        }
        public RegisteredUser? GetRegisteredUserById(int id)
        {
            return _users.GetRegisteredUserById(id);
        }
        public List<RegisteredUser> GetAllRegisteredUsers()
        {
            return _users.GetAllRegisteredUsers();
        }

        public bool IsUsernameUnique(string username)
        {
            foreach (RegisteredUser user in _users.GetAllRegisteredUsers())
                if (user.Account.Username.Equals(username)) return false;

            return true;
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
    }
}
