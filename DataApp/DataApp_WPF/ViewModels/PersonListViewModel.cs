using CommunityToolkit.Mvvm.ComponentModel;

using DataApp_WPF.Models;
using System.Collections.ObjectModel;
using DataApp_WPF.Services;
using DataApp_WPF.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace DataApp_WPF.ViewModels;

public partial class PersonListViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPersonService _personRepo;

    public PersonListViewModel(IServiceProvider serviceProvider, PersonService personRepo)
    {
        _serviceProvider = serviceProvider;
        _personRepo = personRepo;
        People = _personRepo.GetPersonList();
    }

    [ObservableProperty]
    private ObservableCollection<Person> _people;

    [RelayCommand]
    public void ShowPersonDetails(Guid _id)
    {
        MessageBox.Show($"Person has ID: {_id}");
    }
}
