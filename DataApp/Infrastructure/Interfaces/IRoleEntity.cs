using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IRoleEntity
    {
        int Id { get; set; }
        string RoleName { get; set; }
        ICollection<UserRoleEntity> Users { get; set; }
    }
}