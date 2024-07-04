using System;
using System.Collections.Generic;
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

namespace AssociationForProtectionOfAnimals.View.Volunteer
{
    public partial class VolunteerPage : Window, IObserver
    {
        public ObservableCollection<RegisteredUserDTO>? Users { get; set; }

        public class ViewModel
        {
            public ObservableCollection<RegisteredUserDTO> Users { get; set; }
            public ObservableCollection<AdoptionRequestDTO> Requests { get; set; }

            public ViewModel()
            {
                Users = new ObservableCollection<RegisteredUserDTO>();
            }
        }

        private readonly int userId;
        private readonly VolunteerController _volunteerController;
        private readonly IPlaceRepo _placeRepo;

        public RegisteredUserDTO? SelectedUser { get; set; }
        public AdoptionRequestDTO? SelectedRequest { get; set; }
        public ViewModel TableViewModel { get; set; }

        private bool isSearchButtonClicked = false;
        private int currentUserPage = 1;
        private string UsersortCriteria;
        private IUserSortStrategy sortStrategy = new SortByDatetime();

        public VolunteerPage(int UserId)
        {
            InitializeComponent();
            this.userId = UserId;
            _volunteerController = Injector.CreateInstance<VolunteerController>();
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
                UpdatePagination();
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

        private void AcceptRequest_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}