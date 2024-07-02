﻿using AssociationForProtectionOfAnimals.DTO;
using System.Collections.ObjectModel;
using AssociationForProtectionOfAnimals.Observer;
using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.IUtility;
using AssociationForProtectionOfAnimals.Domain.Utility;
using System.Windows;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using System.Windows.Controls;

namespace AssociationForProtectionOfAnimals.View.RegisteredUser
{
    public partial class RegisteredUserPage : Window, IObserver
    {
        public ObservableCollection<PostDTO>? Posts { get; set; }

        public class ViewModel
        {
            public ObservableCollection<PostDTO> Posts { get; set; }

            public ViewModel()
            {
                Posts = new ObservableCollection<PostDTO>();
            }
        }

        private readonly PostController _postController;

        public PostDTO? SelectedPost{ get; set; }
        public ViewModel TableViewModel { get; set; }



        private bool isSearchButtonClicked = false;
        private int currentPostPage= 1;
        private string postSortCriteria = "AnimalBreed";
        private ISortStrategy sortStrategy = new SortByBreed();

        public RegisteredUserPage()
        {
            InitializeComponent();
            _postController = Injector.CreateInstance<PostController>();

            TableViewModel = new ViewModel();
            DataContext = this;
            _postController.Subscribe(this);

            Update();
            UpdatePagination();
        }

        public void Update()
        {
            try
            {
                UpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public void UpdateSearch()
        {
            try
            {
                TableViewModel.Posts.Clear();
                List<Comment> posts = GetFilteredPosts();

                if (posts != null)
                    foreach (Comment post in posts)
                        TableViewModel.Posts.Add(new PostDTO(post));
                else
                    MessageBox.Show("No courses found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void CreatePost_Click(object sender, RoutedEventArgs e)
        {
            /*CreateTeacherFrom createTeacherFrom = new CreateTeacherFrom();
            createTeacherFrom.Show();*/
            Update();
        }

        private void UpdatePost_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPost == null)
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
            if (SelectedPost == null)
                MessageBox.Show("Please choose a post to view!");
            else
            {
                /*Course course = teacherController.GetCourseById(SelectedCourse.Id);
                CourseView courseView = new CourseView(course, directorController.GetById(this.teacherId));
                courseView.Show();*/
            }
        }

        private void SearchPosts_Click(object sender, RoutedEventArgs e)
        {
            UpdateSearch();
            UpdatePagination();
            isSearchButtonClicked = true;
        }

        private void ResetPosts_Click(object sender, EventArgs e)
        {
            isSearchButtonClicked = false;
            Update();
            ResetSearchElements();
            UpdatePagination();
        }

        private void ResetSearchElements()
        {
            postStatusComboBox.SelectedItem = null;
            animalBreedComboBox.SelectedItem = null;
            postStartDateDatePicker.SelectedDate = null;
            minAnimalYearsTextBox.Text = "";
            maxAnimalYearsTextBox.Text = "";
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }

        // ------------------------- PAGINATION -----------------------

        private void PostNextPage_Click(object sender, RoutedEventArgs e)
        {

            currentPostPage++;
            PostPreviousButton.IsEnabled = true;
            UpdatePagination();

        }

        private void PostPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPostPage > 1)
            {
                currentPostPage--;
                PostNextButton.IsEnabled = true;
                UpdatePagination();
            }
            else if (currentPostPage == 1)
            {
                PostPreviousButton.IsEnabled = false;
            }
        }

        public void UpdatePagination()
        {
            if (currentPostPage == 1)
            {
                PostPreviousButton.IsEnabled = false;
            }
            PostPageNumberTextBlock.Text = $"{currentPostPage}";

            try
            {
                TableViewModel.Posts.Clear();
                var filteredPosts = GetFilteredPosts();

                List<Comment> posts = _postController.GetAllPosts(currentPostPage, 4, postSortCriteria, filteredPosts);
                List<Comment> newPosts = _postController.GetAllPosts(currentPostPage + 1, 4, postSortCriteria, filteredPosts);

                if (newPosts.Count == 0)
                    PostNextButton.IsEnabled = false;
                else
                    PostNextButton.IsEnabled = true;
                if (filteredPosts != null)
                {
                    foreach (Comment post in posts)
                        TableViewModel.Posts.Add(new PostDTO(post));
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

        private List<Comment>? GetFilteredPosts()
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

        private List<Comment> GetPostsForDisplay(PostStatus? selectedPostStatus, string selectedBreed, DateTime? selectedStartDate, int selectedMinYears, int selectedMaxYears)
        {
            List<Comment> finalPosts= new();

            if (isSearchButtonClicked)
            {
                List<Comment> allFilteredPosts = _postController.FindPostsByCriteria(selectedPostStatus, selectedBreed, selectedStartDate, selectedMinYears, selectedMaxYears);

                foreach (Comment post in allFilteredPosts)
                    finalPosts.Add(post);
            }
            else
            {
                foreach (Comment post in _postController.GetAllPosts())
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
                        sortStrategy = new SortByDatetime();
                        break;
                    case "PostStatus":
                        postSortCriteria = "PostStatus";
                        sortStrategy = new SortByPostStatus();
                        break;
                    case "AnimalBreed":
                        postSortCriteria = "AnimalBreed";
                        sortStrategy = new SortByBreed();
                        break;
                    case "AnimalYears":
                        postSortCriteria = "AnimalYears";
                        sortStrategy = new SortByAge();
                        break;
                }
                UpdatePagination();
            }
        }
    }
}
