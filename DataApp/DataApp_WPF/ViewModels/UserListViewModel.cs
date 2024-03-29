﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

using Business.Services;
using DataApp_WPF.Models;

namespace DataApp_WPF.ViewModels;

public partial class UserListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserService _userService;

    public UserListViewModel(IServiceProvider serviceProvider, UserService userService)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;

        Task.Run(async () =>
        {
            var listUserDTOs = await _userService.GetUserListAsync();
            ObservableCollection<UserListModel> tempUsers = new ObservableCollection<UserListModel>();

            if (listUserDTOs != null)
            {
                foreach (var userDTO in listUserDTOs)
                {
                    UserListModel user = new UserListModel();
                    user.Id = userDTO.Id;
                    user.UserName = userDTO.UserName;
                    user.Email = userDTO.Email;
                    tempUsers.Add(user);
                }
            }
            else
            {
                UserListModel user = new UserListModel();
                user.UserName = "No users in list!";
                tempUsers.Add(user);
            }

            Users = tempUsers;
        });

        
    }

    [ObservableProperty]
    private ObservableCollection<UserListModel> _users;

    /// <summary>
    /// Stores clicked User Guid in UserService.StoreUserId() and navigates to UserDetailsView
    /// </summary>
    /// <param name="_id">User Guid</param>
    [RelayCommand]
    public void ShowUserDetails(Guid _id)
    {
        _userService.StoreUserId(_id);

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UserDetailsViewModel>();
    }

}
