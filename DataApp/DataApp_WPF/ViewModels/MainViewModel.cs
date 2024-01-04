using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace DataApp_WPF.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    private readonly IServiceProvider? _serviceProvider;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentViewModel = _serviceProvider.GetRequiredService<PersonListViewModel>();
    }

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
}
