using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.Utility
{
    public class SortByDatetime : ISortStrategy
    {
        public IEnumerable<Comment> Sort(IEnumerable<Comment> posts)
        {
            return posts.OrderBy(x => x.DateOfPosting);
        }
    }
}
