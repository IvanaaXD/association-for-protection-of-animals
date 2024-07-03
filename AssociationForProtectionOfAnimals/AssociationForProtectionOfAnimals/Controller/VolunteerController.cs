using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IRepository;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class VolunteerController
    {
        private readonly IVolunteerRepo _volunteers;
        private readonly IRegisteredUserRepo _users;
        private readonly IAdminRepo _admin;
        private readonly IAnimalRepo _animals;
        private readonly IAccountRepo _accounts;
        private readonly IPlaceRepo _places;
        private readonly PostController _posts;

        public VolunteerController()
        {
            _volunteers = Injector.CreateInstance<IVolunteerRepo>();
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
            _admin = Injector.CreateInstance<IAdminRepo>();
            _animals = Injector.CreateInstance<IAnimalRepo>();
            _accounts = Injector.CreateInstance<IAccountRepo>();
            _places = Injector.CreateInstance<IPlaceRepo>();
            _posts = Injector.CreateInstance<PostController>();
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
            Place place = _places.GetPlaceByNameAndPostalCode(user.Place);
            int placeId;
            if (place == null)
                placeId = _places.AddPlace(user.Place).Id;
            else
                placeId = place.Id;
            user.Place.Id = placeId;
            RegisteredUser ret = _volunteers.AddUser(user);
            _accounts.AddAccount(ret.Account);
            return ret;
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

        // Napravljena je funkcija samo za kreiranje posta ako zivotinja nema vlasnika
        public bool AddAnimal(Animal animal, string publisherEmail)
        {
            Post post = new Post(DateTime.Now,DateTime.Now,PostStatus.ForAdoption,false,animal.Id,publisherEmail,null);
            post = _posts.Add(post);
            if (post == null)
                return false;

            return true;
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
            _accounts.Subscribe(observer);
        }
    }
}