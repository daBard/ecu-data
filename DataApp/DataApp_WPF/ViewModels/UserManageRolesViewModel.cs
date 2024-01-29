using Business.DTOs;
using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace DataApp_WPF.ViewModels;

public partial class UserManageRolesViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly RoleService _roleService;


    public UserManageRolesViewModel(IServiceProvider serviceProvider, RoleService roleService)
    {
        _serviceProvider = serviceProvider;
        _roleService = roleService;

        UpdateRoleList();
    }

    public void UpdateRoleList()
    {
        var roleDTOs = _roleService.GetAll();

        ObservableCollection<UserRoleDTO> tempRoles = new ObservableCollection<UserRoleDTO>();

        if (roleDTOs.Any())
        {
            foreach (var roleDTO in roleDTOs)
            {
                tempRoles.Add(roleDTO);
            }
        }
        Roles = tempRoles;
    }

    [ObservableProperty]
    private ObservableCollection<UserRoleDTO> roles;

    [ObservableProperty]
    private string newRoleName;

    [RelayCommand]
    public void DeleteRoleBtn(int id)
    {
        if (MessageBox.Show("This action will remove the role from all users and delete the role.\n\nContinue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
        {
           if (!_roleService.Delete(id))
           {
                MessageBox.Show("Role not deleted!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
           }
           else
                UpdateRoleList();
        }
    }

    [RelayCommand]
    public void AddRoleBtn()
    {
        if (!string.IsNullOrWhiteSpace(NewRoleName))
        {
            if (!_roleService.Create(NewRoleName))
                MessageBox.Show("Role not created!", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                UpdateRoleList();
                NewRoleName = "";
            }
                

        }
        else
            MessageBox.Show("Please enter fields correctly!", "Fields contains null or whitespace", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    [RelayCommand]
    public void BackBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }
}
