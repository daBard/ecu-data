using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRoleEntity
    {
        RoleEntity Role { get; set; }
        int RoleId { get; set; }
        UserEntity User { get; set; }
        Guid UserGuid { get; set; }
    }
}