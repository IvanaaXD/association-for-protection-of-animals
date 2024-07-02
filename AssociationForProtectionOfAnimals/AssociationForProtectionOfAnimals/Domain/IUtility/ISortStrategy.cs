using AssociationForProtectionOfAnimals.Domain.Model;

namespace AssociationForProtectionOfAnimals.Domain.IUtility
{
    public interface ISortStrategy
    {
        IEnumerable<Comment> Sort(IEnumerable<Comment> posts);
    }
}
