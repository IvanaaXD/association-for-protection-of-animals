using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationForProtectionOfAnimals.Domain.IRepository
{
    internal interface IPlaceRepo
    {
        Place GetPlaceById(int id);
        Place GetPlaceByNameAndPostalCode(Place place);
        Place AddPlace(Place place);
    }
}
