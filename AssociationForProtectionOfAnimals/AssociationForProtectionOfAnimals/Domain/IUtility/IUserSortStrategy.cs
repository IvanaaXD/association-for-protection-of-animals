using AssociationForProtectionOfAnimals.Domain.Model;

namespace AssociationForProtectionOfAnimals.Domain.IUtility
{
    public interface IUserSortStrategy
    {
        IEnumerable<RegisteredUser> Sort(IEnumerable<RegisteredUser> registeredUsers);
    }
}
