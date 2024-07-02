using AssociationForProtectionOfAnimals.Domain.IRepository;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Storage;

namespace AssociationForProtectionOfAnimals.Repository
{
    internal class AccountRepo : Subject, IAccountRepo
    {
        private readonly List<Account> _accounts;
        private readonly Storage<Account> _storage;

        public AccountRepo()
        {
            _storage = new Storage<Account>("accounts.csv");
            _accounts = _storage.Load();
        }
        public Account GetAccountById(int id)
        {
            return _accounts.Find(v => v.Id == id);
        }

        public Account AddAccount(Account account)
        {
            _accounts.Add(account);
            _storage.Save(_accounts);
            NotifyObservers();
            return account;
        }
    }
}
