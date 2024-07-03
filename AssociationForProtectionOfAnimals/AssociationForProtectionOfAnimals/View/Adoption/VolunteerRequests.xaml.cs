
using AssociationForProtectionOfAnimals.Domain.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using AssociationForProtectionOfAnimals.DTO;
using System.Collections.ObjectModel;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Utility;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System.Windows.Controls;
using System.Windows.Input;
namespace AssociationForProtectionOfAnimals.View.Adoption
{
    /// <summary>
    /// Interaction logic for VolunteerRequests.xaml
    /// </summary>
    public partial class VolunteerRequests: Window, IObserver
    {
        
        public class ViewModel
        {
            public ObservableCollection<AdoptionRequestDTO> AdoptionRequestDTOs { get; set; }
            public ObservableCollection<TemporaryShelterRequestDTO> TempShelterRequestDTOs { get; set; }

            public ViewModel()
            {
                AdoptionRequestDTOs = new ObservableCollection<AdoptionRequestDTO>();
                TempShelterRequestDTOs = new ObservableCollection<TemporaryShelterRequestDTO>();
            }

        }

        public ViewModel TableViewModel { get; set; }
        public AnimalDTO SelectedAnimal { get; set; }
        private RegisteredUserController regUserController { get; set; }
        private VolunteerController volunteerController { get; set; }
        private PostController postController { get; set; }

        private RequestController requestController { get; set; }

        private int userId { get; set; }
        private bool isSearchButtonClicked = false;

        public VolunteerRequests(int userId)
        {
            InitializeComponent();
            TableViewModel = new ViewModel();
            regUserController = Injector.CreateInstance<RegisteredUserController>();
            volunteerController = Injector.CreateInstance<VolunteerController>();
            postController = Injector.CreateInstance<PostController>();
            requestController = Injector.CreateInstance<RequestController>();   

            this.userId = userId;


            DataContext = this;
            //teacherController.Subscribe(this);
            Update();
        }

        public void Update()
        {
            try
            {
                TableViewModel.AdoptionRequestDTOs.Clear();
                TableViewModel.TempShelterRequestDTOs.Clear();

                var tempShelterRequests = GetFilteredTempShelterRequests();
                var adoptionRequests = GetFilteredAdoptionRequests();
                
                if (tempShelterRequests != null)
                {
                    foreach (Domain.Model.TemporaryShelterRequest tempRequest in tempShelterRequests)
                        TableViewModel.TempShelterRequestDTOs.Add(new TemporaryShelterRequestDTO(tempRequest));
                }
                else if(adoptionRequests != null)
                {
                    foreach (Domain.Model.AdoptionRequest adoptRequest in adoptionRequests)
                        TableViewModel.AdoptionRequestDTOs.Add(new AdoptionRequestDTO(adoptRequest));
                }
                else
                {
                    MessageBox.Show("No requests found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            //private void UpdateExam_Click(object sender, RoutedEventArgs e)

            if (SelectedAnimal == null)
            {
                MessageBox.Show("Please choose animal to adopt!");
            }
            else
            {
                int animalId = SelectedAnimal.Id;
                Post animalPost = null;
                List<Post> posts = postController.GetAllPosts();
                foreach (Post post in posts)
                {
                    if (post.AnimalId == animalId)
                        animalPost = post;
                }
                if (animalPost.PostStatus == PostStatus.Adopted || animalPost.PostStatus == PostStatus.TemporarilyAdopted || animalPost.PostStatus == PostStatus.UnderTreatment)
                {
                    MessageBox.Show("Animal cannot be adopted!");
                }
                else
                {
                    MessageBox.Show("Request sent to volunteers!");
                }
            }

            Close();
        }
       
        private List<Domain.Model.TemporaryShelterRequest> GetFilteredTempShelterRequests()
        {

            List<Domain.Model.TemporaryShelterRequest> finalRequests = new List<Domain.Model.TemporaryShelterRequest>();

            return finalRequests;
        }
        private List<Domain.Model.AdoptionRequest> GetFilteredAdoptionRequests()
        {

            List<Domain.Model.AdoptionRequest> finalRequests = new List<Domain.Model.AdoptionRequest>();

            return finalRequests;
        }
    }
}
