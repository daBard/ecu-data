using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [PrimaryKey(nameof(UserGuid), nameof(RoleId))]
    public class UserRoleEntity
    {
        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserGuid { get; set; }

        [Required]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual UserEntity User { get; set; } = null!;
        public virtual RoleEntity Role { get; set; } = null!;
    }
}
