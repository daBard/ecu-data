using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

using DataApp_WPF.ViewModels;
using DataApp_WPF.Services;
using DataApp_WPF.Views;

namespace DataApp_WPF;

public partial class App : Application
{
    private static IHost? _builder;

    public App()
    {
        _builder = Host.CreateDefaultBuilder()
            .ConfigureServices (services =>
            {
                services.AddSingleton<PersonService>();

                services.AddTransient<PersonListViewModel>();
                services.AddTransient<AddPersonViewModel>();
                services.AddTransient<PersonDetailsViewModel>();
                services.AddTransient<UpdatePersonViewModel>();

                services.AddTransient<PersonListView>();
                services.AddTransient<AddPersonView>();
                services.AddTransient<PersonDetailsView>();
                services.AddTransient<UpdatePersonView>();

                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _builder!.Start();

        var mainWindow = _builder!.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }
}
