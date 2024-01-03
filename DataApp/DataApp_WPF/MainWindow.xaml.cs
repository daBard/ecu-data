using System.Windows;
using DataApp_WPF.ViewModels;

namespace DataApp_WPF;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}