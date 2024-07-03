using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.DTO;
using AssociationForProtectionOfAnimals.Observer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shapes;

namespace AssociationForProtectionOfAnimals.View.UnregisteredUser
{
    public partial class PostView : Window, IObserver
    {
        public ObservableCollection<CommentDTO>? Comments { get; set; }
        public class ViewModel
        {
            public ObservableCollection<CommentDTO>? Comments { get; set; }

            public ViewModel()
            {
                Comments = new ObservableCollection<CommentDTO>();
            }
        }
        public ViewModel CommentsTableViewModel { get; set; }
        public AnimalDTO? SelectedAnimal{ get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly Domain.Model.Animal animal;
        private readonly Domain.Model.RegisteredUser user;
        private readonly Post post;
        private readonly PostController _postController;
        private readonly CommentController _commentController;

        private readonly Window window;

        public PostView(Post post, Domain.Model.RegisteredUser user, Window window)
        {
            InitializeComponent();
            _postController = Injector.CreateInstance<PostController>();
            _commentController = Injector.CreateInstance<CommentController>();

            this.user = user;
            this.post = post;
            this.window = window;

            CommentsTableViewModel = new ViewModel();

            DataContext = this;
            _postController.Subscribe(this);

            Update();

            Closing += PostView_Closing;
        }

        private void PostView_Closing(object? sender, CancelEventArgs e)
        {
            foreach (Window window in Application.Current.Windows.OfType<Window>().Where(w => w != this))
            {
                window.Close();
            }
        }

        public void Update()
        {
            try
            {
                CommentsTableViewModel.Comments?.Clear();
                var comments = _commentController.GetCommentsByPost(post.Id);

                if (comments != null)
                {
                    foreach (Comment comment in comments)
                    {
                        CommentDTO commentDTO = new CommentDTO(comment);
                        CommentsTableViewModel.Comments?.Add(commentDTO);
                    }
                }
                else
                {
                    MessageBox.Show("No teachers found.");
                }

                AddPostInfo();
                AddAnimalInfo();
                CheckButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ViewPosts_Click(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                Owner.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void AddPostInfo()
        {
            postDateOfPostingTextBlock.Text = post.DateOfPosting.ToString("yyyy-MM-dd HH:mm");
            postDateOfUpdatingTextBlock.Text = post.DateOfUpdating.ToString("yyyy-MM-dd HH:mm");
            postStatusTextBlock.Text = $"{post?.PostStatus}";
            postHasCurrentAdopterTextBlock.Text = post?.HasCurrentAdopter.ToString();
            postAuthorTextBlock.Text = GetName(post.Person);
            postAdopterTextBlock.Text = GetName(post.Adopter);
            postNumberOfLikesTextBlock.Text = post.PersonLikedIds.Count.ToString();
        }

        public string GetName(string email)
        {
            RegisteredUserController registeredUserController = Injector.CreateInstance<RegisteredUserController>();
            VolunteerController volunteerController = Injector.CreateInstance<VolunteerController>();
            Person person = registeredUserController.GetRegisteredUserByEmail(post.Adopter);
            if (person == null)
                person = volunteerController.GetVolunteerByEmail(post.Adopter);

            return person.FirstName + " " + person.LastName;
        }

        private void AddAnimalInfo()
        {
            animalNameTextBlock.Text = animal.Name;
            animalAgeTextBlock.Text = animal.Age.ToString();
            animalWeightTextBlock.Text = animal.Weight.ToString() + " kg";
            animalHeightTextBlock.Text = animal.Height.ToString() + " cm";
            animalDescriptionTextBlock.Text = animal.Description;
            animalFoundAdressTextBlock.Text = animal.FoundAddress;
            animalMedicalStatusTextBlock.Text = animal.MedicalStatus;
            animalPlaceTextBlock.Text = animal.Place.Name;
            animalBreedTextBlock.Text = animal.Breed.Name;
            animalSpeciesTextBlock.Text = animal.Species.Name;
        }

        private void CheckButtons()
        {
            if (post.PersonLikedIds.Contains(user.Id))
                Like.Visibility = Visibility.Collapsed;

            if (IsRegisteredUserPage())
            {
                AdoptVolunteer.Visibility = Visibility.Collapsed;
                TemporarilyAdoptVolunteer.Visibility = Visibility.Collapsed;
            }
            else if (IsVolunteerRegistered())
            {
                AdoptRegisteredUser.Visibility = Visibility.Collapsed;
                TemporarilyAdoptRegisteredUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                AdoptRegisteredUser.Visibility = Visibility.Collapsed;
                TemporarilyAdoptRegisteredUser.Visibility = Visibility.Collapsed;
                AdoptVolunteer.Visibility = Visibility.Collapsed;
                TemporarilyAdoptVolunteer.Visibility = Visibility.Collapsed;
                Like.Visibility = Visibility.Collapsed;
            }
        }

        private void LikePost_Click(object sender, RoutedEventArgs e)
        {
            _postController.LikePost(user, post);
            Update();
        }

        private bool IsRegisteredUserPage()
        {
            return window is AssociationForProtectionOfAnimals.View.RegisteredUser.RegisteredUserPage;
        }


        private bool IsVolunteerRegistered()
        {
            return false; //window is AssociationForProtectionOfAnimals.View.Volunteer.VolunteerPage;
        }

        private void AdoptRegisteredUser_Click(object sender, RoutedEventArgs e)
        {
            /*Domain.Model.Student? student = _studentController.GetStudentById(SelectedStudent.id);
            GradeStudentForm gradeStudentForm = new GradeStudentForm(examTerm, teacher, student);

            gradeStudentForm.Closed += RefreshPage;

            gradeStudentForm.Show();
            gradeStudentForm.Activate();*/
        }

        private void AdoptVolunteer_Click(object sender, RoutedEventArgs e)
        {

        }


        private void TemporarilyAdoptRegisteredUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TemporarilyAdoptVolunteer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommentPost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReadComment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshPage(object? sender, EventArgs e)
        {
            Update();
        }
    }
}
