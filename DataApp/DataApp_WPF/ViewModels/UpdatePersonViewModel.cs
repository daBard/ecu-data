using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using DataApp_WPF.Models;
using Business.Services;

namespace DataApp_WPF.ViewModels;

public partial class UpdatePersonViewModel : ObservableObject
{

    private readonly IServiceProvider _serviceProvider;
    private readonly PersonService _personService;

    public UpdatePersonViewModel(IServiceProvider serviceProvider, PersonService personService)
    {
        _serviceProvider = serviceProvider;
        _personService = personService;
        //PersonForm = _personService.GetPerson();
    }

    [ObservableProperty]
    private Person _personForm;

    [RelayCommand]
    public void UpdatePersonBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }

    [RelayCommand]
    public void CancelBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }
}
