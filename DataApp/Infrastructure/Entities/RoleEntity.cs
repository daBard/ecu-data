using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Entities;

public class RoleEntity : IRoleEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRoleEntity> Users { get; set; } = new HashSet<UserRoleEntity>();
}
