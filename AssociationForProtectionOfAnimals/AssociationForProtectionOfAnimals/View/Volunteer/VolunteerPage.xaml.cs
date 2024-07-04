using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.DTO;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Utility;
using AssociationForProtectionOfAnimals.Domain.IRepository;
using AssociationForProtectionOfAnimals.View.UnregisteredUser;
using AssociationForProtectionOfAnimals.View.Animal;

namespace AssociationForProtectionOfAnimals.View.Volunteer
{
    public partial class VolunteerPage : Window, IObserver
    {
        public ObservableCollection<PostDTO>? PublishedPosts { get; set; }
        public ObservableCollection<PostDTO>? UnpublishedPosts { get; set; }

        public ObservableCollection<RegisteredUserDTO>? Users { get; set; }

        public class ViewModel
        {
            public ObservableCollection<PostDTO>? PublishedPosts { get; set; }
            public ObservableCollection<PostDTO>? UnpublishedPosts { get; set; }
            public ObservableCollection<RegisteredUserDTO> Users { get; set; }
            public ObservableCollection<AdoptionRequestDTO> Requests { get; set; }

            public ViewModel()
            {
                Users = new ObservableCollection<RegisteredUserDTO>();
                UnpublishedPosts = new ObservableCollection<PostDTO>();
                PublishedPosts = new ObservableCollection<PostDTO>();
            }
        }

        private readonly int userId;
        private readonly VolunteerController _volunteerController;
        private readonly PostController _postController;

        private readonly IPlaceRepo _placeRepo;

        public RegisteredUserDTO? SelectedUser { get; set; }
        public PostDTO? SelectedPublishedPost { get; set; }
        public PostDTO? SelectedUnpublishedPost { get; set; }

        public ViewModel TableViewModel { get; set; }

        private bool isSearchButtonClicked = false;
        private int currentUserPage = 1;
        private string UsersortCriteria;
        private IUserSortStrategy sortStrategy = new SortByDatetime();

        private int currentPostPage = 1;
        private string postSortCriteria = "AnimalBreed";
        private ISortStrategy postSortStrategy = new SortByBreed();

        public VolunteerPage(int UserId)
        {
            InitializeComponent();
            this.userId = UserId;
            _volunteerController = Injector.CreateInstance<VolunteerController>();
            _postController = Injector.CreateInstance<PostController>();
            _placeRepo = Injector.CreateInstance<IPlaceRepo>();

            TableViewModel = new ViewModel();
            DataContext = this;
            _volunteerController.Subscribe(this);

            Update();
            UpdatePagination();
        }

        public void Update()
        {
            try
            {
                SetUsers();
                SetPosts();
                UpdatePagination();
                UpdatePostPagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        private void SetUsers()
        {
            TableViewModel.Users.Clear();
            var users = _volunteerController.GetAllRegisteredUsers();

            if (users != null)
            {
                foreach (Domain.Model.RegisteredUser user in users)
                {
                    TableViewModel.Users.Add(new RegisteredUserDTO(user));
                }
            }
        }

        private void SetPosts()
        {
            TableViewModel.UnpublishedPosts.Clear();
            TableViewModel.PublishedPosts.Clear();
            var publishedPosts = _postController.GetAllPublishedPosts();
            var unpublishedPosts = _postController.GetAllPublishedPosts();

            if (publishedPosts != null)
                foreach (Post post in publishedPosts)
                    TableViewModel.PublishedPosts.Add(new PostDTO(post));

            if (unpublishedPosts != null)
                foreach (Post post in unpublishedPosts)
                    TableViewModel.UnpublishedPosts.Add(new PostDTO(post));
        }

        private void SearchUsers_Click(object sender, RoutedEventArgs e)
        {
            UpdateSearch();
            UpdatePagination();
            isSearchButtonClicked = true;
        }

        public void UpdateSearch()
        {
            try
            {
                TableViewModel.Users.Clear();
                List<Domain.Model.RegisteredUser> Users = GetFilteredUsers();

                if (Users != null)
                {
                    foreach (Domain.Model.RegisteredUser User in Users)
                    {
                        TableViewModel.Users.Add(new RegisteredUserDTO(User));
                    }
                }
                else
                {
                    MessageBox.Show("No Users found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ResetUsers_Click(object sender, RoutedEventArgs e)
        {
            isSearchButtonClicked = false;
            Update();
            ResetSearchElements();
            UpdatePagination();
        }

        private void ResetSearchElements()
        {
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            placeTextBox.Text = string.Empty;
            dateOfBirthDatePicker.SelectedDate = null;
        }

        private List<Domain.Model.RegisteredUser> GetFilteredUsers()
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            Place place = _placeRepo.GetPlaceByName(placeTextBox.Text);
            DateTime? dateOfBirth = dateOfBirthDatePicker.SelectedDate;

            return _volunteerController.FindRegisteredUsersByCriteria(firstName, lastName, place, dateOfBirth);
        }

        private void UserNextPage_Click(object sender, RoutedEventArgs e)
        {
            currentUserPage++;
            UserPreviousButton.IsEnabled = true;
            UpdatePagination();
        }

        private void UserPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentUserPage > 1)
            {
                currentUserPage--;
                UserNextButton.IsEnabled = true;
                UpdatePagination();
            }
            else if (currentUserPage == 1)
            {
                UserPreviousButton.IsEnabled = false;
            }
        }

        private void UpdatePagination()
        {
            if (currentUserPage == 1)
            {
                UserPreviousButton.IsEnabled = false;
            }
            UserPageNumberTextBlock.Text = $"{currentUserPage}";

            try
            {
                TableViewModel.Users.Clear();
                var filteredUsers = GetFilteredUsers();
                List<Domain.Model.RegisteredUser> Users = _volunteerController.GetAllRegisteredUsers(currentUserPage, 2, sortStrategy, filteredUsers);
                List<Domain.Model.RegisteredUser> newUsers = _volunteerController.GetAllRegisteredUsers(currentUserPage + 1, 2, sortStrategy, filteredUsers);

                if (newUsers.Count == 0)
                {
                    UserNextButton.IsEnabled = false;
                }
                else
                {
                    UserNextButton.IsEnabled = true;
                }

                if (filteredUsers != null)
                {
                    foreach (Domain.Model.RegisteredUser User in Users)
                    {
                        TableViewModel.Users.Add(new RegisteredUserDTO(User));
                    }
                }
                else
                {
                    MessageBox.Show("No Users found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void UsersortCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersortCriteriaComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = selectedItem.Content.ToString();
                switch (selectedContent)
                {
                    case "DateOfBirth":
                        UsersortCriteria = "DateOfBirth";
                        sortStrategy = new SortByDatetime();
                        break;
                }
                UpdatePagination();
            }
        }

        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
           
        }

        // -------------------------------------- POSTS -------------------------------------------

        private void CreatePost_Click(object sender, RoutedEventArgs e)
        {
            Animal.CreateAnimal createAnimal = new CreateAnimal(userId);
            createAnimal.Show();
            Update();
        }

        private void UpdatePost_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPublishedPost == null)
                MessageBox.Show("Please choose a post to update!");
            else
            {
                /*UpdateTeacherForm updateTeacherForm = new UpdateTeacherForm(SelectedTeacher.Id);
                updateTeacherForm.Show();
                updateTeacherForm.Activate();*/
                Update();
            }
        }

        private void ViewPost_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPublishedPost == null)
                MessageBox.Show("Please choose a post to view!");
            else
            {
                PostView postView = new PostView(SelectedPublishedPost.ToPost(), new Domain.Model.RegisteredUser(), this);
                postView.Show();
                postView.Activate();
                Update();
            }
        }

        public void UpdatePostSearch()
        {
            try
            {
                TableViewModel.PublishedPosts.Clear();
                List<Post> posts = GetFilteredPosts();

                if (posts != null)
                    foreach (Post post in posts)
                        TableViewModel.PublishedPosts.Add(new PostDTO(post));
                else
                    MessageBox.Show("No courses found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void SearchPosts_Click(object sender, RoutedEventArgs e)
        {
            UpdatePostSearch();
            UpdatePostPagination();
            isSearchButtonClicked = true;
        }

        private void ResetPosts_Click(object sender, EventArgs e)
        {
            isSearchButtonClicked = false;
            Update();
            ResetPostSearchElements();
            UpdatePostPagination();
        }

        private void ResetPostSearchElements()
        {
            postStatusComboBox.SelectedItem = null;
            animalBreedComboBox.SelectedItem = null;
            postStartDateDatePicker.SelectedDate = null;
            minAnimalYearsTextBox.Text = "";
            maxAnimalYearsTextBox.Text = "";
        }

        private void PostNextPage_Click(object sender, RoutedEventArgs e)
        {

            currentPostPage++;
            PostPreviousButton.IsEnabled = true;
            UpdatePostPagination();

        }

        private void PostPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPostPage > 1)
            {
                currentPostPage--;
                PostNextButton.IsEnabled = true;
                UpdatePostPagination();
            }
            else if (currentPostPage == 1)
            {
                PostPreviousButton.IsEnabled = false;
            }
        }

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            if (SelectedUnpublishedPost == null)
                MessageBox.Show("Please choose a post to update!");
            else
            {
                _volunteerController.AcceptPostRequest(SelectedUnpublishedPost.ToPost());
                Update();
            }
=======
            _volunteerController.AcceptPostRequest(SelectedUnpublishedPost.ToPost());
>>>>>>> Stashed changes
        }
        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            if (SelectedUnpublishedPost == null)
                MessageBox.Show("Please choose a post to update!");
            else
            {
                _volunteerController.RejectPostRequest(SelectedUnpublishedPost.ToPost());
                Update();
            }
=======
            _volunteerController.RejectPostRequest(SelectedUnpublishedPost.ToPost());
>>>>>>> Stashed changes
        }

        public void UpdatePostPagination()
        {
            if (currentPostPage == 1)
            {
                PostPreviousButton.IsEnabled = false;
            }
            PostPageNumberTextBlock.Text = $"{currentPostPage}";

            try
            {
                TableViewModel.PublishedPosts.Clear();
                var filteredPosts = GetFilteredPosts();

                List<Post> posts = _postController.GetAllPosts(currentPostPage, 4, postSortCriteria, filteredPosts);
                List<Post> newPosts = _postController.GetAllPosts(currentPostPage + 1, 4, postSortCriteria, filteredPosts);

                if (newPosts.Count == 0)
                    PostNextButton.IsEnabled = false;
                else
                    PostNextButton.IsEnabled = true;
                if (filteredPosts != null)
                {
                    foreach (Post post in posts)
                        TableViewModel.PublishedPosts.Add(new PostDTO(post));
                }
                else
                {
                    MessageBox.Show("No exam terms found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private List<Post>? GetFilteredPosts()
        {
            PostStatus? selectedPostStatus = PostStatus.NULL;
            string? selectedBreed = null;
            DateTime? selectedStartDate = DateTime.MinValue;
            int selectedMinYears = 0;
            int selectedMaxYears = 0;

            if (!string.IsNullOrEmpty(minAnimalYearsTextBox.Text))
            {
                if (int.TryParse(minAnimalYearsTextBox.Text, out int duration))
                {
                    selectedMinYears = duration;
                }
            }

            if (!string.IsNullOrEmpty(maxAnimalYearsTextBox.Text))
            {
                if (int.TryParse(maxAnimalYearsTextBox.Text, out int duration))
                {
                    selectedMaxYears = duration;
                }
            }

            if (postStatusComboBox.SelectedItem != null)
                selectedPostStatus = (PostStatus)postStatusComboBox.SelectedItem;

            if (animalBreedComboBox.SelectedItem != null)
                selectedBreed = animalBreedComboBox.SelectedItem.ToString();

            if (postStartDateDatePicker.SelectedDate.HasValue)
                selectedStartDate = (DateTime)postStartDateDatePicker.SelectedDate;

            return GetPostsForDisplay(selectedPostStatus, selectedBreed, selectedStartDate, selectedMinYears, selectedMaxYears);
        }

        private List<Post> GetPostsForDisplay(PostStatus? selectedPostStatus, string selectedBreed, DateTime? selectedStartDate, int selectedMinYears, int selectedMaxYears)
        {
            List<Post> finalPosts = new();

            if (isSearchButtonClicked)
            {
                List<Post> allFilteredPosts = _postController.FindPostsByCriteria(selectedPostStatus, selectedBreed, selectedStartDate, selectedMinYears, selectedMaxYears);

                foreach (Post post in allFilteredPosts)
                    finalPosts.Add(post);
            }
            else
            {
                foreach (Post post in _postController.GetAllPublishedPosts())
                    finalPosts.Add(post);
            }

            return finalPosts;
        }

        private void PostSortCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (postSortCriteriaComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = selectedItem.Content.ToString();
                switch (selectedContent)
                {
                    case "DateOfPosting":
                        postSortCriteria = "DateOfPosting";
                        postSortStrategy = new SortByDatetime();
                        break;
                    case "PostStatus":
                        postSortCriteria = "PostStatus";
                        postSortStrategy = new SortByPostStatus();
                        break;
                    case "AnimalBreed":
                        postSortCriteria = "AnimalBreed";
                        postSortStrategy = new SortByBreed();
                        break;
                    case "AnimalYears":
                        postSortCriteria = "AnimalYears";
                        postSortStrategy = new SortByAge();
                        break;
                }
                UpdatePostPagination();
            }
        }
    }
}