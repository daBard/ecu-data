using DataApp_WPF.Interfaces;

namespace DataApp_WPF.Models;

public class Person() : IPerson
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
