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

    [RelayCommand]
    public void AddUserBtn()
    {
        //This needs regex instead
        if (!string.IsNullOrWhiteSpace(CreateUserForm.UserName) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.Email) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.Password) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.FirstName) && 
            !string.IsNullOrWhiteSpace(CreateUserForm.LastName))
        {
            UserAddDTO newUser = new UserAddDTO
            {
                UserName = CreateUserForm.UserName,
                Email = CreateUserForm.Email,
                Password = CreateUserForm.Password,
                FirstName = CreateUserForm.FirstName,
                LastName = CreateUserForm.LastName
            };

            if (!_userService.CreateUser(newUser))
                MessageBox.Show("New user not created!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

            NavigateToUserList();
        }
        else
        {
            MessageBox.Show("Please enter fields correctly!", "Fields contains null or whitespace", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

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