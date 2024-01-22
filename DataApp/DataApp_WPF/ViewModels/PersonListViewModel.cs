using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

using DataApp_WPF.Models;
using Business.Services;

namespace DataApp_WPF.ViewModels;

public partial class PersonListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly PersonService _personService;

    public PersonListViewModel(IServiceProvider serviceProvider, PersonService personService)
    {
        _serviceProvider = serviceProvider;
        _personService = personService;
        //People = _personService.GetPersonList();
    }

    [ObservableProperty]
    private ObservableCollection<Person> _people;

    [RelayCommand]
    public void ShowPersonDetails(Guid _id)
    {
        _personService.StoreId(_id);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonDetailsViewModel>();    
    }
}
