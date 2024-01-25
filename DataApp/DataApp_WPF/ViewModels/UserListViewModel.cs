using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

using Business.Services;
using DataApp_WPF.Models;

namespace DataApp_WPF.ViewModels
{
    public partial class UserListViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserService _userService;

        public UserListViewModel(IServiceProvider serviceProvider, UserService userService)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;

            //Users = new ObservableCollection<ListUser>(_userService.GetUserList());
        }

        [ObservableProperty]
        private ObservableCollection<ListUser> _users;

        [RelayCommand]
        public void ShowUserDetails(Guid _id)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonDetailsViewModel>();
        }

    }
}
