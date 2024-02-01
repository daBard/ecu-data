using Business.DTOs;
using Infrastructure.Entities;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        bool Create(string roleName);
        bool Delete(int id);
        RoleEntity Exists(string roleName);
        IEnumerable<RoleDTO> GetAll();
    }
}