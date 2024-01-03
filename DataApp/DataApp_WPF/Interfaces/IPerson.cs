namespace DataApp_WPF.Interfaces
{
    public interface IPerson
    {
        string FirstName { get; set; }
        Guid Id { get; }
        string LastName { get; set; }
    }
}