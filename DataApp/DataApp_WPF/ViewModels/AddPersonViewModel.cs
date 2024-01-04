using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

using DataApp_WPF.Models;
using DataApp_WPF.Services;
using DataApp_WPF.Interfaces;

namespace DataApp_WPF.ViewModels;

public partial class AddPersonViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public AddPersonViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [ObservableProperty]
    private Person personForm = new();

    [RelayCommand]
    public void AddPersonBtn()
    {
        if (!string.IsNullOrWhiteSpace(PersonForm.FirstName))
        {
            IPersonService personService = _serviceProvider.GetRequiredService<PersonService>();
            personService.AddPersonToList(PersonForm);

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
        }
    }

    [RelayCommand]
    public void CancelBtn()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }
}
