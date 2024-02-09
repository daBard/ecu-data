using Business.DTOs;
using Infrastructure.Entities;

namespace Business.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateAsync(string roleName);
        Task<bool> DeleteAsync(int id);
        Task<RoleEntity> ExistsAsync(string roleName);
        Task<IEnumerable<RoleDTO>> GetAllAsync();
    }
}