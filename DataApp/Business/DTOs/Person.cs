namespace Business.DTOs;

public class Person()
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }
    public int? Age { get; set; } = null;
    public string FavouriteFood { get; set; } = null!;
}
