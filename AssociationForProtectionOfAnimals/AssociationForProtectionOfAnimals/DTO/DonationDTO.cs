using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AssociationForProtectionOfAnimals.DTO
{
    public class DonationDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        private DateTime dateOfDonation;
        private int value;
        private string authorFirstName;
        private string pdfFilePath;

        private string authorLastName;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public DateTime DateOfDonation
        {
            get { return dateOfDonation; }
            set { SetProperty(ref dateOfDonation, value); }
        }

        public int Value
        {
            get { return value; }
            set { SetProperty(ref value, value); }
        }

        public string AuthorFirstName
        {
            get { return authorFirstName; }
            set { SetProperty(ref authorFirstName, value); }
        }

        public string PdfFilePath
        {
            get { return pdfFilePath; }
            set { SetProperty(ref pdfFilePath, value); }
        }

        public string AuthorLastName
        {
            get { return authorLastName; }
            set { SetProperty(ref authorLastName, value); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    // Example validation
                    // case "Value":
                    //     if (value <= 0)
                    //         return "Donation value must be greater than 0";
                    //     break;
                    default:
                        return null;
                }
            }
        }

        public Donation ToDonation()
        {
            return new Donation
            {
                Id = id,
                DateOfDonation = dateOfDonation,
                Value = value,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName,
                PdfFilePath = pdfFilePath,
            };
        }

        public DonationDTO() { }

        public DonationDTO(Donation donation)
        {
            id = donation.Id;
            dateOfDonation = donation.DateOfDonation;
            value = donation.Value;
            authorFirstName = donation.AuthorFirstName;
            authorLastName = donation.AuthorLastName;
            pdfFilePath = donation.PdfFilePath;
        }
    }
}
