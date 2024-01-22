using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class UserRoleEntity
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int RoleId { get; set; }

        public virtual UserEntity User { get; set; } = null!;
        public virtual RoleEntity Role { get; set; } = null!;
    }
}
