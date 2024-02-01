using Business.DTOs;

namespace Business.Interfaces
{
    public interface IUserRoleService
    {
        bool UpdateUserRoles(IEnumerable<RoleDTO> roleDTOs, Guid userGuid);
        IEnumerable<RoleDTO> GetUserRoles(Guid guid);
    }
}