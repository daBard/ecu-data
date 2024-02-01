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

    private readonly ConnectionStrings _connectionStrings = new ConnectionStrings();

    public App()
    {
        _builder = Host.CreateDefaultBuilder()
            .ConfigureServices (services =>
            {
                services.AddDbContext<LocalUserDataContext>(x => x.UseSqlServer(_connectionStrings.Work));

                services.AddScoped<ErrorLogger>();
                services.AddScoped<UserService>();
                services.AddScoped<RoleService>();
                services.AddScoped<UserRoleService>();
                services.AddScoped<UserRepo>();
                services.AddScoped<RoleRepo>();
                services.AddScoped<AddressRepo>();
                services.AddScoped<UserProfileRepo>();
                services.AddScoped<UserRoleRepo>();

                services.AddTransient<UserListViewModel>();
                services.AddTransient<UserAddViewModel>();
                services.AddTransient<UserDetailsViewModel>();
                services.AddTransient<UserUpdateViewModel>();
                services.AddTransient<UserManageRolesViewModel>();

                services.AddTransient<UserListView>();
                services.AddTransient<UserAddView>();
                services.AddTransient<UserDetailsView>();
                services.AddTransient<UserUpdateView>();
                services.AddTransient<UserManageRolesView>();

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
