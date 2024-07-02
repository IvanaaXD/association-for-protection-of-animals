using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Model;

namespace AssociationForProtectionOfAnimals.Domain.Utility
{
    public class SortByPostStatus : ISortStrategy
    {
        public IEnumerable<Comment> Sort(IEnumerable<Comment> posts)
        {
            return posts.OrderBy(x => x.PostStatus);
        }
    }
}
