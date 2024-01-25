using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.EntityFrameworkCore;

using DataApp_WPF.ViewModels;
using DataApp_WPF.Views;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Helper;
using Business.Services;



namespace DataApp_WPF;

public partial class App : Application
{
    private static IHost? _builder;

    //private readonly List<string> _connectionStrings = new List<string>
    //{
    //    // 0 HOME COMPUTER
    //    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bardj\Documents\GitHub\ecu-data\DataApp\Infrastructure\Data\User_LocalDb.mdf;Integrated Security=True;Connect Timeout=30",
    //    // 1 WORK LAPTOP
    //    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\jnbd05\Documents\Barden-GitHub\ECU\data\DataApp\Infrastructure\Data\User_LocalDb.mdf;Integrated Security=True;Connect Timeout=30",
    //    // 2 MARIA DB
    //    @""
    //};

    private readonly ConnectionStrings _connectionStrings = new ConnectionStrings();

    public App()
    {
        _builder = Host.CreateDefaultBuilder()
            .ConfigureServices (services =>
            {
                services.AddDbContext<LocalDataContext>(x => x.UseSqlServer(_connectionStrings.Home));

                services.AddSingleton<ErrorLogger>();
                services.AddSingleton<UserService>();
                services.AddSingleton<UserRepo>();

                services.AddTransient<UserListViewModel>();
                //services.AddTransient<AddPersonViewModel>();
                //services.AddTransient<PersonDetailsViewModel>();
                //services.AddTransient<UpdatePersonViewModel>();

                services.AddTransient<UserListView>();
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
