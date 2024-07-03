using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class RegisteredUserController : Subject
    {
        private readonly IRegisteredUserRepo _users;
        private readonly IAnimalRepo _animals;
        private readonly IAccountRepo _account;
        private readonly IPlaceRepo _place;
        private readonly IRequestRepo _request;

        public RegisteredUserController()
        {
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
            _account = Injector.CreateInstance<IAccountRepo>();
            _place = Injector.CreateInstance<IPlaceRepo>();
            _animals = Injector.CreateInstance<IAnimalRepo>();
            _request = Injector.CreateInstance<IRequestRepo>();
        }

        public void Add(RegisteredUser user)
        {
            /* SEND REQUEST FOR REGISTRATION */
            Place place = _place.GetPlaceByNameAndPostalCode(user.Place);
            int placeId;
            if (place == null)
                placeId = _place.AddPlace(user.Place).Id;
            else
                placeId = place.Id;
            user.Place.Id = placeId;
            RegisteredUser ret = _users.AddRegisteredUser(user);
            _account.AddAccount(ret.Account);
            NotifyObservers();
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
            _account.Subscribe(observer);
        }
        public RegisteredUser? GetRegisteredUserById(int id)
        {
            return _users.GetRegisteredUserById(id);
        }
        public List<RegisteredUser> GetAllRegisteredUsers()
        {
            return _users.GetAllRegisteredUsers();
        }
        public Account GetAccountById(int id)
        {
            return _account.GetAccountById(id);
        }

        public RegisteredUser GetRegisteredUserByEmail(string email)
        {
            return _users.GetUserByEmail(email);
        }

        public bool IsUsernameUnique(string username)
        {
            foreach (RegisteredUser user in _users.GetAllRegisteredUsers())
                if (user.Account.Username.Equals(username)) return false;

            return true;
        }
        public Animal AddAnimal(Animal animal, int userId)
        {
            Animal newAnimal = _animals.AddAnimal(animal);
            if (newAnimal == null)
                return null;
            Request adoptionRequest = new Request(userId, 0, Domain.Model.Enums.RequestStatus.WaitingForResponse, DateTime.Now);
            _request.AddRequest(adoptionRequest);
            return newAnimal;
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
