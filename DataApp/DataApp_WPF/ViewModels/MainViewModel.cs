using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using Business.Services;
using Helper;

namespace DataApp_WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider? _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<UserListViewModel>();
    }

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [RelayCommand]
    private void NavigateToAddUser()
    {
        CurrentViewModel = _serviceProvider!.GetRequiredService<UserAddViewModel>();
    }

    [RelayCommand]
    public void NavigateToUserList()
    {
        CurrentViewModel = _serviceProvider!.GetRequiredService<UserListViewModel>();
    }

    [RelayCommand]
    public static void Help()
    {
        MessageBox.Show("\"Many things can be a waste of your effort, but a helping hand is not.\"\n\n" +
            "Now you are inspired... GET BACK TO CODING!\n\n" +
            "Oh, and the icons come from https://fluenticons.co/", "In many forms, help can come...", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
