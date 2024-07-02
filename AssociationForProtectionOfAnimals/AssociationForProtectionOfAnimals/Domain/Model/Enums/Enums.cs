
namespace AssociationForProtectionOfAnimals.Domain.Model.Enums
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum AccountType
    {
        RegisteredUser,
        Volunteer,
        Admin
    }

    public enum PostStatus
    {
        NULL,
        ForAdoption,
        Adopted,
        TemporarilyAdopted,
        UnderTreatment
    }


    public enum TypeOfRequest
    {
        UpdateAccountRequest,
        RegistrationRequest,
        AdoptionRequest,
        TemporaryAdoptionRequest,
        AddAnimalRequest,
        UpdateAnimalRequest
    }

    public enum RequestStatus
    {
        WaitingForResponse,
        Accepted,
        Denied,
    }

    public enum TypeOfDonation
    {
        Individual,
        Group,
        General,
    }
}
