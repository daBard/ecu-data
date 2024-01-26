using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using Business.Services;
using DataApp_WPF.Models;
using Helper;
using System.Windows;


namespace DataApp_WPF.ViewModels;

public partial class UserDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;
    private readonly ErrorLogger _errorLogger;

    private readonly Guid _userId;

    public UserDetailsViewModel(IServiceProvider serviceProvider, UserService userService, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _errorLogger = errorLogger;

        _userId = _userService.GetStoredUserId();

        var userDetailsDTO = _userService.GetUserDetails(_userId);

        if (userDetailsDTO != null )
        {
            UserDetailsModel userDetails = new UserDetailsModel()
            {
                UserName = userDetailsDTO.UserName,
                Email = userDetailsDTO.Email,
                RegistrationDate = userDetailsDTO.RegistrationDate,
                FirstName = userDetailsDTO.FirstName,
                LastName = userDetailsDTO.LastName,
                Street = userDetailsDTO.Street,
                PostalCode = userDetailsDTO.PostalCode,
                City = userDetailsDTO.City
            };

            UserDetailsForm = userDetails;
        }
        else
        {
            _errorLogger.Logger("UserDetailsViewModel.Constructor", "UserDetailsForm is null");
            UserDetailsForm = null!;
        }
    }

    [ObservableProperty]
    private UserDetailsModel userDetailsForm;

    [RelayCommand]
    public void UpdateUserBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserUpdateViewModel>();

    }

    [RelayCommand]
    public void ToListBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();

    }

    [RelayCommand]
    public void DeleteUserBtn()
    {
        if (!_userService.DeleteUser())
        {
            MessageBox.Show("\"User could not be deleted. See log for more information.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
        }
    }
}
