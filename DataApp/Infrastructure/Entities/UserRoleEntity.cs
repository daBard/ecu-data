using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    [PrimaryKey(nameof(UserId), nameof(RoleId))]
    public class UserRoleEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual UserEntity User { get; set; } = null!;
        public virtual RoleEntity Role { get; set; } = null!;
    }
}
