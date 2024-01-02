namespace DataApp_WPF.Mvvm.Models;

public class Item(string name)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = name;
}
