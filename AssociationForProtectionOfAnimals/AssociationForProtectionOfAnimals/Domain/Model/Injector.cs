using AssociationForProtectionOfAnimals.Controller;
using System;
using System.Collections.Generic;
using AssociationForProtectionOfAnimals.Domain.IRepository;
using AssociationForProtectionOfAnimals.Repository;

namespace AssociationForProtectionOfAnimals.Domain.Model
{
    public static class Injector
    {
        private static Dictionary<Type, Lazy<object>> _implementations = new Dictionary<Type, Lazy<object>>
        {
            { typeof(IRegisteredUserRepo), new Lazy<object>(() => new RegisteredUserRepo()) },
            { typeof(RegisteredUserController), new Lazy<object>(() => new RegisteredUserController()) },
            { typeof(IVolunteerRepo), new Lazy<object>(() => new VolunteerRepo()) },
            { typeof(VolunteerController), new Lazy<object>(() => new VolunteerController()) },
            { typeof(IAdminRepo), new Lazy<object>(() => new AdminRepo()) },
            { typeof(AdministratorController), new Lazy<object>(() => new AdministratorController()) },
            { typeof(IAnimalRepo), new Lazy<object>(() => new AnimalRepo()) },

        };

        static Injector()
        {
            /*Data.AppDbContext appDbContext = new Data.AppDbContext();   
            _implementations.Add(typeof(IPenaltyPointRepo), new Lazy<object>(() => new PenaltyPointRepo()));
            _implementations.Add(typeof(PenaltyPointController), new Lazy<object>(() => new PenaltyPointController()));
            _implementations.Add(typeof(ReportController), new Lazy<object>(() => new ReportController()));
            _implementations.Add(typeof(IExamTermDbRepo), new Lazy<object>(() => new ExamTermDbRepo(appDbContext))); //
            _implementations.Add(typeof(ICourseDbRepo), new Lazy<object>(() => new CourseDbRepo(appDbContext)));     //
            _implementations.Add(typeof(IAdminDbRepo), new Lazy<object>(() => new AdminDbRepo(appDbContext)));*/

        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type].Value;
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
