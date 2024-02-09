using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Threading;

using Helper;
using Business.DTOs;
using Business.Services;
using DataApp_WPF.Models;

namespace DataApp_WPF.ViewModels;

public partial class UserUpdateViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly UserRoleService _userRoleService;
    private readonly ErrorLogger _errorLogger;
   
    private readonly UserDetailsDTO _userDetailsDTO = null!;

    internal IEnumerable<RoleDTO> _roleDTOs = null!;
    internal List<RoleDTO> _userRoleDTOs = null!;

    public UserUpdateViewModel(IServiceProvider serviceProvider, UserService userService, RoleService roleService, UserRoleService userRoleService, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _roleService = roleService;
        _userRoleService = userRoleService;
        _errorLogger = errorLogger;

        _userDetailsDTO = _userService.GetStoredUserDetailsDTO();

        Task.Run(async () =>
        {
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


                var tempUserRoleDTOs = new List<RoleDTO>(await _userRoleService.GetUserRolesAsync(_userDetailsDTO.Guid));
                _userRoleDTOs = tempUserRoleDTOs;

            }
            else
            {
                _errorLogger.Logger("UserDetailsViewModel.Constructor", "UserDetailsForm is null");
                UserUpdateDetailsForm = null!;
            }

            _roleDTOs = await _roleService.GetAllAsync();

            UpdateRoleLists();
        });
    }

    /// <summary>
    /// Gets all Avaliable Roles to List and all User Roles to List
    /// Removes User Roles in User Roles List from Avaliable Roles List
    /// Updates both Lists in view
    /// </summary>
    public void UpdateRoleLists()
    {
        ObservableCollection<RoleDTO> tempRoles = new ObservableCollection<RoleDTO>();
        ObservableCollection<RoleDTO> tempUserRoles = new ObservableCollection<RoleDTO>(_userRoleDTOs);

        if (_roleDTOs.Any())
        {
            foreach (var roleDTO in _roleDTOs)
            {
                if (!_userRoleDTOs.Any(x => x.Id == roleDTO.Id))
                    tempRoles.Add(roleDTO);
            }
        }

        Roles = tempRoles;
        UserRoles = tempUserRoles;
    }

    [ObservableProperty]
    private UserDetailsModel userUpdateDetailsForm = null!;

    [ObservableProperty]
    private ObservableCollection<RoleDTO> userRoles = null!;

    [ObservableProperty]
    private ObservableCollection<RoleDTO> roles = null!;

    /// <summary>
    /// Adds clicked Role from Avaliable Roles to UserRoles
    /// </summary>
    /// <param name="id">Role id as int binded to Avaliable Roles list item in view</param>
    [RelayCommand]
    public void AddRoleBtn(int id)
    {
        RoleDTO addedRoleDTO = _roleDTOs.FirstOrDefault(x => x.Id == id)!;

        _userRoleDTOs.Add(addedRoleDTO);

        UpdateRoleLists();
    }

    /// <summary>
    /// Removes the clicked Role from UserRoles in view
    /// </summary>
    /// <param name="id">Role id as int binded to User Roles list item in view</param>
    [RelayCommand]
    public void RemoveRoleBtn(int id)
    {
        _userRoleDTOs.RemoveAll(x => x.Id == id);

        UpdateRoleLists();
    }

    /// <summary>
    /// Gathers user input from view and checks if required inputs has content
    /// Creates UserDetailsDTO and sends to UserService.UpdateUserDetails()
    /// Navigates to UserDetailsView if successful, else fail Messagebox
    /// </summary>
    [RelayCommand]
    public async void UpdateUserBtn()
    {
        bool success = false;

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

            success = await _userService.UpdateUserDetailsAsync(updatedUser);

            success = await _userRoleService.UpdateUserRolesAsync(_userRoleDTOs, _userDetailsDTO.Guid);

            if (success)
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

    /// <summary>
    /// Navigates to UserDetailsView
    /// </summary>
    [RelayCommand]
    public void CancelBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();
    }
}
