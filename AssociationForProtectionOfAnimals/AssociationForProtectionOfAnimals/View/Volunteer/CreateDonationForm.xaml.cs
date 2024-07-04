using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.DTO;
using Microsoft.Win32;
using System.Windows;

namespace AssociationForProtectionOfAnimals.View.Volunteer
{
    public partial class CreateDonationForm : Window
    {
        public DonationDTO Donation { get; set; }
        private readonly DonationController _donationController;

        public CreateDonationForm()
        {
            InitializeComponent();
            Donation = new DonationDTO();
            _donationController = Injector.CreateInstance<DonationController>();
            DataContext = this;
        }

        private void BrowsePdfFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Donation.PdfFilePath = openFileDialog.FileName;
            }
        }

        private void SaveDonation_Click(object sender, RoutedEventArgs e)
        {
            Donation donation = new()
            {
                DateOfDonation = Donation.DateOfDonation,
                AuthorFirstName = Donation.AuthorFirstName,
                AuthorLastName = Donation.AuthorLastName,
                Value = Donation.Value,
                PdfFilePath = Donation.PdfFilePath
            };

            _donationController.Add(donation);
            DialogResult = true;
            Close();
        }
    }
}
