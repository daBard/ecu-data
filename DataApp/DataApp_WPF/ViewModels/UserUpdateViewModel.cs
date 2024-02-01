using CommunityToolkit.Mvvm.ComponentModel;

using Helper;
using Business.DTOs;
using Business.Services;
using DataApp_WPF.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Collections.ObjectModel;

namespace DataApp_WPF.ViewModels;

public partial class UserUpdateViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly UserRoleService _userRoleService;
    private readonly ErrorLogger _errorLogger;
   
    private readonly UserDetailsDTO _userDetailsDTO;

    public UserUpdateViewModel(IServiceProvider serviceProvider, UserService userService, RoleService roleService, UserRoleService userRoleService, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _roleService = roleService;
        _userRoleService = userRoleService;
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

            var userRolesDTO = _userRoleService.GetUserRoles(_userDetailsDTO.Guid);

            UserUpdateDetailsForm = userDetails;
        }
        else
        {
            _errorLogger.Logger("UserDetailsViewModel.Constructor", "UserDetailsForm is null");
            UserUpdateDetailsForm = null!;
        }

        var avaliableRolesDTO = _roleService.GetAll();
        
        ObservableCollection<RoleDTO> avaliableRoleList = new ObservableCollection<RoleDTO>(avaliableRolesDTO);

        UpdateRoleLists();

    }

    [ObservableProperty]
    private UserDetailsModel userUpdateDetailsForm;

    [ObservableProperty]
    private ObservableCollection<RoleDTO> userRoles;

    [ObservableProperty]
    private ObservableCollection<RoleDTO> roles;

    [RelayCommand]
    public void AddRoleBtn(int id)
    {
        if (_userRoleService.AddRoleToUser(_userDetailsDTO.Guid, id))
        {
            UpdateRoleLists();
        }
        else
            MessageBox.Show("Role could not be added to user!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    [RelayCommand]
    public void RemoveRoleBtn(int id)
    {
        if (_userRoleService.RemoveRoleFromUser(_userDetailsDTO.Guid, id))
        {
            UpdateRoleLists();
        }
        else
            MessageBox.Show("Role could not be removed from user!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void UpdateRoleLists()
    {
        var roleDTOs = _roleService.GetAll();
        var userRoleDTOs = _userRoleService.GetUserRoles(_userDetailsDTO.Guid);

        ObservableCollection<RoleDTO> tempRoles = new ObservableCollection<RoleDTO>();
        ObservableCollection<RoleDTO> tempUserRoles = new ObservableCollection<RoleDTO>(userRoleDTOs);

        if (roleDTOs.Any())
        {
            foreach (var roleDTO in roleDTOs)
            {
                if (!userRoleDTOs.Any(x => x.Id == roleDTO.Id))
                    tempRoles.Add(roleDTO);
            }
        }

        Roles = tempRoles;
        UserRoles = tempUserRoles;
    }

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
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();
    }
}
