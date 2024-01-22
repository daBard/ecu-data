using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.EntityFrameworkCore;

using DataApp_WPF.ViewModels;
using DataApp_WPF.Views;
using Infrastructure.Contexts;
using Helper;



namespace DataApp_WPF;

public partial class App : Application
{
    private static IHost? _builder;

    private readonly string _localConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bardj\Documents\GitHub\ecu-data\DataApp\Infrastructure\Data\User_LocalDb.mdf;Integrated Security=True;Connect Timeout=30";
    private readonly string _mariaConnectionString = "";

    public App()
    {
        _builder = Host.CreateDefaultBuilder()
            .ConfigureServices (services =>
            {
                services.AddDbContext<LocalDataContext>(x => x.UseSqlServer(_localConnectionString));

                services.AddSingleton<ErrorLogger>();

                //services.AddTransient<PersonListViewModel>();
                //services.AddTransient<AddPersonViewModel>();
                //services.AddTransient<PersonDetailsViewModel>();
                //services.AddTransient<UpdatePersonViewModel>();

                //services.AddTransient<PersonListView>();
                //services.AddTransient<AddPersonView>();
                //services.AddTransient<PersonDetailsView>();
                //services.AddTransient<UpdatePersonView>();

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
