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
    /// Interaction logic for RegisteredUserPage.xaml
    /// </summary>
    public partial class RegisteredUserPage : Window, IObserver
    {
        public class ViewModel
        {
            public ObservableCollection<AnimalDTO> Animals { get; set; }

            public ViewModel()
            {
                Animals = new ObservableCollection<AnimalDTO>();
            }

        }

        public ViewModel TableViewModel { get; set; }
        public AnimalDTO SelectedAnimal { get; set; }
        private RegisteredUserController regUserController { get; set; }
        private VolunteerController volunteerController { get; set; }
        private PostController postController { get; set; }

        private int userId { get; set; }
        private bool isSearchButtonClicked = false;

        public RegisteredUserPage(int userId)
        {
            InitializeComponent();
            TableViewModel = new ViewModel();
            regUserController = Injector.CreateInstance<RegisteredUserController>();
            volunteerController = Injector.CreateInstance<VolunteerController>();
            postController = Injector.CreateInstance<PostController>();

            this.userId = userId;

            //languageComboBox.ItemsSource = Enum.GetValues(typeof(Language));
            //levelComboBox.ItemsSource = Enum.GetValues(typeof(LanguageLevel));

            DataContext = this;
            //teacherController.Subscribe(this);
            Update();
        }
        
        public void Update()
        {
            try
            {
                TableViewModel.Animals.Clear();
                var animals = GetFilteredAnimals();

                if (animals != null)
                {
                    foreach (Domain.Model.Animal animal in animals)
                        TableViewModel.Animals.Add(new AnimalDTO(animal));
                }
                else
                {
                    MessageBox.Show("No animals found.");
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
        private void btnAdopt_Click(object sender, EventArgs e)
        {
            //private void UpdateExam_Click(object sender, RoutedEventArgs e)
            
            if (SelectedAnimal == null)
            {
                MessageBox.Show("Please choose animal to adopt!");
            }
            else
            {
                int animalId = SelectedAnimal.Id;
                Post animalPost=null;
                List<Post> posts = postController.GetAllPosts();
                foreach (Post post in posts) { 
                    if (post.AnimalId == animalId) 
                         animalPost = post;  
                }
                if (animalPost.PostStatus==PostStatus.Adopted || animalPost.PostStatus==PostStatus.TemporarilyAdopted || animalPost.PostStatus==PostStatus.UnderTreatment)
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Update();
            isSearchButtonClicked = true;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            isSearchButtonClicked = false;
            Update();
            ResetSearchElements();
        }

        private void ResetSearchElements()
        {
            languageComboBox.SelectedItem = null;
            levelComboBox.SelectedItem = null;
            startDateDatePicker.SelectedDate = null;
        }
        
        private List<Domain.Model.Animal> GetFilteredAnimals()
        {
            /*Language? selectedLanguage = (Language?)languageComboBox.SelectedItem;
            LanguageLevel? selectedLevel = (LanguageLevel?)levelComboBox.SelectedItem;
            DateTime? selectedStartDate = startDateDatePicker.SelectedDate;

            List<ExamTerm> studentsAvailableExamTerms = studentsController.GetAvailableExamTerms(studentId);*/
            List<Domain.Model.Animal> finalAnimals = new List<Domain.Model.Animal>();
            /*
            if (isSearchButtonClicked)
            {
                List<ExamTerm> allFilteredExamTerms = examTermController.FindExamTermsByCriteria(selectedLanguage, selectedLevel, selectedStartDate);

                foreach (ExamTerm examTerm in allFilteredExamTerms)
                {
                    foreach (ExamTerm studentExamTerm in studentsAvailableExamTerms)
                    {
                        if (studentExamTerm.ExamID == examTerm.ExamID && !finalExamTerms.Contains(examTerm))
                        {
                            finalExamTerms.Add(examTerm);
                        }
                    }
                }
            }
            else
            {
                foreach (ExamTerm studentExamTerm in studentsAvailableExamTerms)
                {
                    finalExamTerms.Add(studentExamTerm);
                }
            }*/
            return finalAnimals;
        }
    }
}
