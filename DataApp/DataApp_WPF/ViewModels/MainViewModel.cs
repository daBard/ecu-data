﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

using Business.Services;
using Helper;

namespace DataApp_WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider? _serviceProvider;
    private readonly ErrorLogger? _errorLogger;

    private readonly TestService _testService = new TestService();

    public MainViewModel(IServiceProvider serviceProvider, ErrorLogger errorLogger)
    {
        _serviceProvider = serviceProvider;
        _errorLogger = errorLogger;
        //CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();

        errorLogger.Logger("Test method", "Test message");
    }

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [RelayCommand]
    private void NavigateToAddPerson()
    {
        CurrentViewModel = _serviceProvider!.GetRequiredService<AddPersonViewModel>();
    }

    [RelayCommand]
    public void NavigateToPersonList()
    {
        CurrentViewModel = _serviceProvider!.GetRequiredService<PersonListViewModel>();
    }

    [RelayCommand]
    public static void Help()
    {
        MessageBox.Show("There is no help for you!\n\nOh, and the icons come from https://fluenticons.co/", "Help is for the weak!", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
