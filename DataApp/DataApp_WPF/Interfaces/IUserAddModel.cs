namespace DataApp_WPF.Interfaces
{
    public interface IUserAddModel
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }
}