using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserProfileEntity
    {
        DateTime Birthdate { get; set; }
        string FirstName { get; set; }
        int Id { get; set; }
        string LastName { get; set; }
        UserEntity Users { get; set; }
    }
}