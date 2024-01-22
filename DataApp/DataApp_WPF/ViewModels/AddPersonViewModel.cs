using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using DataApp_WPF.Models;
using Business.Services;
using System.Windows;

namespace DataApp_WPF.ViewModels;

public partial class AddPersonViewModel(IServiceProvider serviceProvider, PersonService personService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly PersonService _personService = personService;

    [ObservableProperty]
    private Person personForm = new();

    [RelayCommand]
    public void AddPersonBtn()
    {
        if (!string.IsNullOrWhiteSpace(PersonForm.FirstName) && !string.IsNullOrWhiteSpace(PersonForm.LastName) && !string.IsNullOrWhiteSpace(PersonForm.FavouriteFood))
        {
            //_personService.AddPersonToList(PersonForm);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
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
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }
}
