using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class UserEntity
{
    [Key]
    public Guid Guid { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string UserName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "date")]
    public DateTime RegistrationDate { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string Password { get; set; } = null!;

    public virtual UserProfileEntity UserProfile { get; set; } = null!;

    public ICollection<UserRoleEntity> UserRoles { get; set; } = new HashSet<UserRoleEntity>();

}
