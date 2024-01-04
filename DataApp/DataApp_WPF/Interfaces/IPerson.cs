namespace DataApp_WPF.Interfaces
{
    public interface IPerson
    {
        Guid Id { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        int Age { get; set; }
        string FavouriteFood { get; set; }
    }
}