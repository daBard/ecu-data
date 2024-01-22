using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class LocalDataContext : DbContext
{
    public LocalDataContext(DbContextOptions<LocalDataContext> options) : base(options)
    {   
    }

    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<UserProfileEntity> UserProfiles { get; set; }
    public virtual DbSet<UserRoleEntity> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>()
            .HasIndex(x => x.RoleName)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}
