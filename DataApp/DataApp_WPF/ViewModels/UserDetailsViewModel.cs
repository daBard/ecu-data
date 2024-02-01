using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using Business.Services;
using DataApp_WPF.Models;
using Helper;
using System.Windows;
using Business.DTOs;
using System.Collections.ObjectModel;


namespace DataApp_WPF.ViewModels;

public partial class UserDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;
    private readonly UserRoleService _userRoleService;
    private readonly ErrorLogger _errorLogger;

    private readonly Guid _userId;

    public UserDetailsViewModel(IServiceProvider serviceProvider, UserService userService, UserRoleService userRoleService, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _userRoleService = userRoleService;
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

        var userRolesDTOs = _userRoleService.GetUserRoles(_userId);

        if (userRolesDTOs != null)
        {
            UserRoles = new ObservableCollection<RoleDTO>(userRolesDTOs);
        }
    }

    [ObservableProperty]
    private UserDetailsModel userDetailsForm;

    [ObservableProperty]
    private ObservableCollection<RoleDTO> userRoles;

    /// <summary>
    /// Navigates to UserUpdateView
    /// </summary>
    [RelayCommand]
    public void UpdateUserBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserUpdateViewModel>();

    }

    /// <summary>
    /// Navigates to UserListView
    /// </summary>
    [RelayCommand]
    public void ToListBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();

    }

    /// <summary>
    /// Deletes User shown in view
    /// Navigates to UserListView if successful, else fail MessageBox
    /// </summary>
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
