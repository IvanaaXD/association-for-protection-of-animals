using System.Collections.Generic;
using LangLang.Repository;
using LangLang.Observer;
using LangLang.Domain.Model;
using LangLang.Domain.IRepository;
using System;

namespace LangLang.Controller
{
    public class RegisteredUserController : Subject
    {
        private readonly IRegisteredUserRepo _users;

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
                if (user.Username.Equals(username)) return false;

            return true;
        }
    }
}
