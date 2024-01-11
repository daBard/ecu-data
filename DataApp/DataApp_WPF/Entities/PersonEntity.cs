namespace DataApp_WPF.Entities;

public class PersonEntity
{
    Guid Guid { get; set; }
    string FirstName { get; set; } = null!;
    string LastName { get; set; } = null!;
    int Age { get; set; }
}
