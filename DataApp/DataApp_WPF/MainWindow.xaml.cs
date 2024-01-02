using System.Collections.ObjectModel;
using System.Windows;
using DataApp_WPF.Mvvm.Models;

namespace DataApp_WPF;

public partial class MainWindow : Window
{
    // Initialize the list with some data
    //public List<string> MyStringList {  get; set; }
    public ObservableCollection<Item> Items { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        Items = new ObservableCollection<Item>();

        Items.Add(new Item("Strumpa"));
        Items.Add(new Item("Byxa"));
        Items.Add(new Item("Kebab"));

        myListBox.ItemsSource = Items;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Hello, WPF!");
    }
}