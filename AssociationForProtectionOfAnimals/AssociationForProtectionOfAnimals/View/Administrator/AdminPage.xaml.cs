using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.DTO;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Utility;
using AssociationForProtectionOfAnimals.Domain.IRepository;

namespace AssociationForProtectionOfAnimals.View.Administrator
{
    public partial class AdminPage : Window, IObserver
    {
        public ObservableCollection<RegisteredUserDTO>? Users { get; set; }
        public ObservableCollection<RegisteredUserDTO>? Volunteers { get; set; }

        public class ViewModel
        {
            public ObservableCollection<RegisteredUserDTO> Users { get; set; }
            public ObservableCollection<RegisteredUserDTO>? Volunteers { get; set; }


            public ViewModel()
            {
                Users = new ObservableCollection<RegisteredUserDTO>();
                Volunteers = new ObservableCollection<RegisteredUserDTO>();
            }
        }

        private readonly VolunteerController _volunteerController;
        private readonly AdministratorController _adminController;
        private readonly IPlaceRepo _placeRepo;

        public RegisteredUserDTO? SelectedUser { get; set; }
        public RegisteredUserDTO? SelectedVolunteer { get; set; }
        public ViewModel TableViewModel { get; set; }

        private bool isSearchButtonClicked = false;
        private bool isSearchVolunteerButtonClicked = false;
        private int currentUserPage = 1;
        private int currentVolunteerPage = 1;
        private string UserSortCriteria;
        private string VolunteerSortCriteria;

        private IUserSortStrategy sortStrategy = new SortByDatetime();
        private IUserSortStrategy sortVolunteerStrategy = new SortByDatetime();

        public AdminPage()
        {
            InitializeComponent();
            _volunteerController = Injector.CreateInstance<VolunteerController>();
            _placeRepo = Injector.CreateInstance<IPlaceRepo>();
            _adminController = Injector.CreateInstance<AdministratorController>();

            TableViewModel = new ViewModel();
            DataContext = this;
            _volunteerController.Subscribe(this);
            _adminController.Subscribe(this);

            Update();
            UpdatePagination();
        }

        public void Update()
        {
            try
            {
                SetUsers();
                SetVolunteers();
                UpdatePagination();
                UpdateVolunteerPagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
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
        private void SetVolunteers()
        {
            TableViewModel.Volunteers.Clear();
            var volunteers = _adminController.GetAllVolunteers();

            if (volunteers != null)
            {
                foreach (Domain.Model.Volunteer volunteer in volunteers)
                {
                    TableViewModel.Volunteers.Add(new RegisteredUserDTO(volunteer));
                }
            }
        }

        private void SearchUsers_Click(object sender, RoutedEventArgs e)
        {
            UpdateSearch();
            UpdatePagination();
            isSearchButtonClicked = true;
        }
        private void SearchVolunteers_Click(object sender, RoutedEventArgs e)
        {
            UpdateVolunteerSearch();
            UpdateVolunteerPagination();
            isSearchVolunteerButtonClicked = true;
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
        public void UpdateVolunteerSearch()
        {
            try
            {
                TableViewModel.Volunteers.Clear();
                List<Domain.Model.Volunteer> volunteers = GetFilteredVolunteers();

                if (volunteers != null)
                {
                    foreach (Domain.Model.Volunteer volunteer in volunteers)
                    {
                        TableViewModel.Volunteers.Add(new RegisteredUserDTO(volunteer));
                    }
                }
                else
                {
                    MessageBox.Show("No volunteers found.");
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
        private void ResetVolunteers_Click(object sender, RoutedEventArgs e)
        {
            isSearchVolunteerButtonClicked = false;
            Update();
            ResetVolunteerSearchElements();
            UpdateVolunteerPagination();
        }

        private void ResetSearchElements()
        {
            firstNameTextBox.Text = string.Empty;
            lastNameTextBox.Text = string.Empty;
            placeTextBox.Text = string.Empty;
            dateOfBirthDatePicker.SelectedDate = null;
        }
        private void ResetVolunteerSearchElements()
        {
            volFirstNameTextBox.Text = string.Empty;
            volLastNameTextBox.Text = string.Empty;
            volPlaceTextBox.Text = string.Empty;
            volDateOfBirthDatePicker.SelectedDate = null;
        }

        private List<Domain.Model.RegisteredUser> GetFilteredUsers()
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            Place place = _placeRepo.GetPlaceByName(placeTextBox.Text);
            DateTime? dateOfBirth = dateOfBirthDatePicker.SelectedDate;

            return _volunteerController.FindRegisteredUsersByCriteria(firstName, lastName, place, dateOfBirth);
        }

        private List<Domain.Model.Volunteer> GetFilteredVolunteers()
        {
            string firstName = volFirstNameTextBox.Text;
            string lastName = volLastNameTextBox.Text;
            Place place = _placeRepo.GetPlaceByName(volPlaceTextBox.Text);
            DateTime? dateOfBirth = volDateOfBirthDatePicker.SelectedDate;

            return _adminController.FindVolunteersByCriteria(firstName, lastName, place, dateOfBirth);
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

        private void VolunteerNextPage_Click(object sender, RoutedEventArgs e)
        {
            currentVolunteerPage++;
            VolunteerPreviousButton.IsEnabled = true;
            UpdateVolunteerPagination();
        }

        private void VolunteerPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentVolunteerPage > 1)
            {
                currentVolunteerPage--;
                VolunteerNextButton.IsEnabled = true;
                UpdatePagination();
            }
            else if (currentVolunteerPage == 1)
            {
                VolunteerPreviousButton.IsEnabled = false;
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
        private void UpdateVolunteerPagination()
        {
            if (currentVolunteerPage == 1)
            {
                VolunteerPreviousButton.IsEnabled = false;
            }
            VolunteerPageNumberTextBlock.Text = $"{currentVolunteerPage}";

            try
            {
                TableViewModel.Volunteers.Clear();
                var filteredVolunteers = GetFilteredVolunteers();
                List<Domain.Model.Volunteer> volunteers = _adminController.GetAllVolunteers(currentUserPage, 2, sortVolunteerStrategy, filteredVolunteers);
                List<Domain.Model.Volunteer> newVolunteers = _adminController.GetAllVolunteers(currentUserPage + 1, 2, sortVolunteerStrategy, filteredVolunteers);

                if (newVolunteers.Count == 0)
                {
                    VolunteerNextButton.IsEnabled = false;
                }
                else
                {
                    VolunteerNextButton.IsEnabled = true;
                }

                if (filteredVolunteers != null)
                {
                    foreach (Domain.Model.Volunteer volunteer in volunteers)
                    {
                        TableViewModel.Volunteers.Add(new RegisteredUserDTO(volunteer));
                    }
                }
                else
                {
                    MessageBox.Show("No volunters found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void UserSortCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserSortCriteriaComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = selectedItem.Content.ToString();
                switch (selectedContent)
                {
                    /*case "FirstName":
                        UsersortCriteria = "FirstName";
                        sortStrategy = new SortByFirstName();
                        break;
                    case "LastName":
                        UsersortCriteria = "LastName";
                        sortStrategy = new SortByLastName();
                        break;*/
                    case "DateOfBirth":
                        UserSortCriteria = "DateOfBirth";
                        sortStrategy = new SortByDatetime();
                        break;
                    /*case "IsBlackListed":
                        UsersortCriteria = "IsBlackListed";
                        sortStrategy = new SortByDatetime();
                        break;*/
                }
                UpdatePagination();
            }
        }

        private void VolunteerSortCriteriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VolunteerSortCriteriaComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedContent = selectedItem.Content.ToString();
                switch (selectedContent)
                {
                    /*case "FirstName":
                        UsersortCriteria = "FirstName";
                        sortStrategy = new SortByFirstName();
                        break;
                    case "LastName":
                        UsersortCriteria = "LastName";
                        sortStrategy = new SortByLastName();
                        break;*/
                    case "DateOfBirth":
                        VolunteerSortCriteria = "DateOfBirth";
                        sortVolunteerStrategy = new SortByDatetime();
                        break;
                        /*case "IsBlackListed":
                            UsersortCriteria = "IsBlackListed";
                            sortStrategy = new SortByDatetime();
                            break;*/
                }
                UpdatePagination();
            }
        }

        private void Suggestion_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateVolunteer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}