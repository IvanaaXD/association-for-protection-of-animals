﻿using AssociationForProtectionOfAnimals.Controller;
using AssociationForProtectionOfAnimals.Domain.Model;
using AssociationForProtectionOfAnimals.Domain.Model.Enums;
using AssociationForProtectionOfAnimals.DTO;
using System.Windows;
using System.Windows.Controls;

namespace AssociationForProtectionOfAnimals.View.UnregisteredUser
{
    public partial class RegistrationForm : Window
    {
        public Gender[] genderValues => (Gender[])Enum.GetValues(typeof(Gender));

        public UserDTO user { get; set; }

        private RegisteredUserController registeredUserController;

        public RegistrationForm()
        {
            InitializeComponent();
            user = new UserDTO();
            user.Password = passwordBox.Password;
            registeredUserController = Injector.CreateInstance<RegisteredUserController>();
            DataContext = this;

            SetPlaceholders();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (user.IsValid)
            {
                if (registeredUserController.IsUsernameUnique(user.Username))
                {
                    registeredUserController.Add(user.ToRegisteredUser());
                    Close();
                }
                else
                {
                    MessageBox.Show("Username is taken.");
                }
            }
            else
            {
                MessageBox.Show("User can not be created. Not all fields are valid.");
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                user.Password = passwordBox.Password;
            }
        }
        private void SetPlaceholders()
        {
            user.FirstName = "Name";
            user.LastName = "Surname";
            user.Username = "exampleUsername123";
            user.PhoneNumber = "0123456789";
            user.Password = "password123";
            user.HomeAddress = "address example";
            user.IdNumber = "1234567898765";
            passwordBox.Password = user.Password;

            firstNameTextBox.GotFocus += FirstNameTextBox_GotFocus;
            lastNameTextBox.GotFocus += LastNameTextBox_GotFocus;
            usernameTextBox.GotFocus += UsernameTextBox_GotFocus;
            phoneNumberTextBox.GotFocus += PhoneNumberTextBox_GotFocus;
            passwordBox.GotFocus += PasswordBox_GotFocus;
            addressTextBox.GotFocus += AddressTextBox_GotFocus;
            IdNumberTextBox.GotFocus += IdNumberTextBox_GotFocus;
        }

        private void FirstNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            firstNameTextBox.Text = string.Empty;
        }

        private void LastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            lastNameTextBox.Text = string.Empty;
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = string.Empty;
        }

        private void PhoneNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            phoneNumberTextBox.Text = string.Empty;
        }
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = string.Empty;
        }

        private void AddressTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            addressTextBox.Text = string.Empty;
        }

        private void IdNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            IdNumberTextBox.Text = string.Empty;
        }

    }
}
