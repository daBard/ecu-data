using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserEntity
    {
        AddressEntity Address { get; set; }
        int AddressId { get; set; }
        string Email { get; set; }
        Guid Guid { get; set; }
        string Password { get; set; }
        string SecurityKey { get; set; }
        DateTime RegistrationDate { get; set; }
        ICollection<UserRoleEntity> Roles { get; set; }
        string UserName { get; set; }
        UserProfileEntity UserProfile { get; set; }
        int UserProfileId { get; set; }
    }
}