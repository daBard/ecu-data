using CommunityToolkit.Mvvm.ComponentModel;

using Helper;
using Business.DTOs;
using Business.Services;
using DataApp_WPF.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DataApp_WPF.ViewModels;

public partial class UserUpdateViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;
    private readonly ErrorLogger _errorLogger;

    private readonly UserDetailsDTO _userDetailsDTO;

    public UserUpdateViewModel(IServiceProvider serviceProvider, UserService userService, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _errorLogger = errorLogger;

        _userDetailsDTO = _userService.GetStoredUserDetailsDTO();

        if (_userDetailsDTO != null)
        {
            UserDetailsModel userDetails = new UserDetailsModel()
            {
                UserName = _userDetailsDTO.UserName,
                Email = _userDetailsDTO.Email,
                RegistrationDate = _userDetailsDTO.RegistrationDate,
                FirstName = _userDetailsDTO.FirstName,
                LastName = _userDetailsDTO.LastName,
                Street = _userDetailsDTO.Street,
                PostalCode = _userDetailsDTO.PostalCode,
                City = _userDetailsDTO.City
            };

            UserUpdateDetailsForm = userDetails;
        }
        else
        {
            _errorLogger.Logger("UserDetailsViewModel.Constructor", "UserDetailsForm is null");
            UserUpdateDetailsForm = null!;
        }
    }

    [ObservableProperty]
    private UserDetailsModel userUpdateDetailsForm;

    [RelayCommand]
    public void UpdateUserBtn()
    {
        if (!string.IsNullOrWhiteSpace(UserUpdateDetailsForm.UserName) &&
        !string.IsNullOrWhiteSpace(UserUpdateDetailsForm.Email) &&
        !string.IsNullOrWhiteSpace(UserUpdateDetailsForm.FirstName) &&
        !string.IsNullOrWhiteSpace(UserUpdateDetailsForm.LastName))
        {
            UserDetailsDTO updatedUser = _userDetailsDTO;

            updatedUser.FirstName = UserUpdateDetailsForm.FirstName;
            updatedUser.LastName = UserUpdateDetailsForm.LastName;
            updatedUser.Street = UserUpdateDetailsForm.Street;
            updatedUser.PostalCode = UserUpdateDetailsForm.PostalCode;
            updatedUser.City = UserUpdateDetailsForm.City;

            if (_userService.UpdateUserDetails(updatedUser))
            {
                var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();
            }
            else
                MessageBox.Show("User not updated!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MessageBox.Show("Please enter fields correctly!", "Fields contains null or whitespace", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    public void CancelBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}
