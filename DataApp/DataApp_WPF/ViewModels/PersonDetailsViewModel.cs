using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using DataApp_WPF.Interfaces;
using DataApp_WPF.Services;
using DataApp_WPF.Models;

namespace DataApp_WPF.ViewModels;

public partial class PersonDetailsViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly PersonService _personService;

    public PersonDetailsViewModel(IServiceProvider serviceProvider, PersonService personService)
    {
        _serviceProvider = serviceProvider;
        _personService = personService;
        PersonDetails = _personService.GetPerson();
    }

    [ObservableProperty]
    private Person _personDetails;

    [RelayCommand]
    private void DeletePersonBtn()
    {
        _personService.DeletePersonFromList();
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }

    [RelayCommand]
    private void UpdatePersonBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdatePersonViewModel>();
    }
}
