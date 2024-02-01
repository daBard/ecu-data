namespace Business.Interfaces
{
    public interface IUserAddDTO
    {
        string Email { get; set; }
        string FirstName { get; set; }
        Guid Id { get; }
        string LastName { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }
}