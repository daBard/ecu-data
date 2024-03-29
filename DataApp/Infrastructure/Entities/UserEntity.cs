﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Interfaces;

namespace Infrastructure.Entities;

//!! Password should be its own Entity
public class UserEntity : IUserEntity
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

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string SecurityKey { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(UserProfile))]
    public int UserProfileId { get; set; }

    [Required]
    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual UserProfileEntity UserProfile { get; set; } = null!;

    public virtual AddressEntity Address { get; set; } = null!;

    public virtual ICollection<UserRoleEntity> Roles { get; set; } = new HashSet<UserRoleEntity>();

}
