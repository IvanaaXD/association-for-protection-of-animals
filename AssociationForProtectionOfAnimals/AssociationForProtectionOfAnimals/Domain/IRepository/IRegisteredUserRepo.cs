using LangLang.Domain.Model;
using LangLang.Observer;

namespace LangLang.Domain.IRepository
{
    public interface IRegisteredUserRepo
    {
        int GenerateId();
        RegisteredUser AddRegisteredUser(RegisteredUser user);
        RegisteredUser? UpdateRegisteredUser(RegisteredUser user);
        RegisteredUser? RemoveRegisteredUser(int id);
        RegisteredUser? GetRegisteredUserById(int id);
        List<RegisteredUser> GetAllRegisteredUsers();
        void Subscribe(IObserver observer);
    }
}
