using Business.DTOs;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using DataApp_WPF.Models;

namespace DataApp_WPF.ViewModels;

public partial class UserAddViewModel(IServiceProvider serviceProvider, UserService userService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly UserService _userService = userService;


    [ObservableProperty]
    private UserAddModel createUserForm = new();

    /// <summary>
    /// Gathers user input from view and checks if required inputs has content
    /// Creates UserAddDTO and sends to UserService.CreateUser()
    /// Navigates to UserListView if successful, else fail Messagebox
    /// </summary>
    [RelayCommand]
    public void AddUserBtn()
    {
        //!! This needs regex for Email, Password and ConformPassword
        if (!string.IsNullOrWhiteSpace(CreateUserForm.UserName) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.Email) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.Password) &&
            !string.IsNullOrWhiteSpace(CreateUserForm.ConfirmPassword) &&
            !string.IsNullOrWhiteSpace(CreateUserForm.FirstName) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.LastName))
        {
            if (CreateUserForm.Password == CreateUserForm.ConfirmPassword)
            {
                byte[] encData_byte = new byte[CreateUserForm.Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(CreateUserForm.Password);
                string encodedData = Convert.ToBase64String(encData_byte);

                UserAddDTO newUser = new UserAddDTO
                {
                    UserName = CreateUserForm.UserName,
                    Email = CreateUserForm.Email,
                    Password = encodedData,
                    ConfirmPassword = CreateUserForm.ConfirmPassword,
                    FirstName = CreateUserForm.FirstName,
                    LastName = CreateUserForm.LastName
                };

                if (!_userService.CreateUser(newUser))
                    MessageBox.Show("New user not created!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                NavigateToUserList();
            }
            else
                MessageBox.Show("Password is not the same as Confirm Password!", "Password Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        else
            MessageBox.Show("Please enter fields correctly!", "Fields contains null or whitespace", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// Navigates to UserListView
    /// </summary>
    [RelayCommand]
    public void CancelBtn()
    {
        NavigateToUserList();
    }

    private void NavigateToUserList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}