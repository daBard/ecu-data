using Business.DTOs;

namespace Business.Interfaces
{
    public interface IUserRoleService
    {
        bool AddRoleToUser(Guid userGuid, int roleId);
        IEnumerable<RoleDTO> GetUserRoles(Guid guid);
        bool RemoveRoleFromUser(Guid userGuid, int roleId);
    }
}