using Business.DTOs;

namespace Business.Interfaces
{
    public interface IUserRoleService
    {
        Task<bool> UpdateUserRolesAsync(IEnumerable<RoleDTO> roleDTOs, Guid userGuid);
        Task<IEnumerable<RoleDTO>> GetUserRolesAsync(Guid guid);
    }
}