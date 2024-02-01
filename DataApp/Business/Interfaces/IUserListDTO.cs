namespace Business.Interfaces
{
    public interface IUserListDTO
    {
        string Email { get; set; }
        Guid Id { get; set; }
        string UserName { get; set; }
    }
}