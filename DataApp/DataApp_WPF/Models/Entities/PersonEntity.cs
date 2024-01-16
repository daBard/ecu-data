namespace DataApp_WPF.Entities;

public class PersonEntity
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public string FavouriteFood { get; set; } = null!;
}
