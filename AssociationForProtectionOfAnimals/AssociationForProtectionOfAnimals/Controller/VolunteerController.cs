using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.Controller
{
    public class VolunteerController
    {
        private readonly IVolunteerRepo _volunteers;
        private readonly IRegisteredUserRepo _users;
        private readonly IAdminRepo _admin;

        public VolunteerController()
        {
            _volunteers = Injector.CreateInstance<IVolunteerRepo>();
            _users = Injector.CreateInstance<IRegisteredUserRepo>();
            _admin = Injector.CreateInstance<IAdminRepo>();
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


        public void Subscribe(IObserver observer)
        {
            _volunteers.Subscribe(observer);
            _users.Subscribe(observer);
        }
    }
}