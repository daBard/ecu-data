using Business.Interfaces;

namespace Business.DTOs;

public class RoleDTO : IRoleDTO
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;
}
