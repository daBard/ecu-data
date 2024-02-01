using Business.DTOs;
using Business.Interfaces;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class UserRoleService : IUserRoleService
{
    private readonly UserRoleRepo _userRoleRepo;
    private readonly RoleRepo _roleRepo;
    private readonly ErrorLogger _errorLogger;

    public UserRoleService(UserRoleRepo userRoleRepo, RoleRepo roleRepo, ErrorLogger errorLogger)
    {
        _userRoleRepo = userRoleRepo;
        _roleRepo = roleRepo;
        _errorLogger = errorLogger;
    }

    /// <summary>
    /// Adds a role to a user, returns bool
    /// </summary>
    /// <param name="userGuid">User Guid</param>
    /// <param name="roleId">Role Id to be added</param>
    /// <returns>True if successful, else false</returns>
    public bool AddRoleToUser(Guid userGuid, int roleId)
    {
        try
        {
            UserRoleEntity userRoleEntity = new UserRoleEntity()
            {
                UserGuid = userGuid,
                RoleId = roleId
            };
            if (_userRoleRepo.Create(userRoleEntity) != null)
            {
                return true;
            }
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Gets an IEnumerable of a Users Roles
    /// </summary>
    /// <param name="guid">User Guid</param>
    /// <returns>True if successful, else false</returns>
    public IEnumerable<RoleDTO> GetUserRoles(Guid guid)
    {
        try
        {
            var roles = _roleRepo.GetAll();
            var userRoleEntities = _userRoleRepo.GetAllFromGuid(x => x.UserGuid == guid);

            List<RoleDTO> userRoles = new List<RoleDTO>();
            RoleEntity roleEntity = new RoleEntity();

            foreach (UserRoleEntity userRoleEntity in userRoleEntities)
            {
                roleEntity = roles.FirstOrDefault(x => x.Id == userRoleEntity.RoleId);

                RoleDTO tempRoleDTO = new RoleDTO()
                {
                    Id = userRoleEntity.RoleId,
                    RoleName = roleEntity.RoleName
                };

                userRoles.Add(tempRoleDTO);
            }

            return userRoles;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
    }

    /// <summary>
    /// Removes a role from a user, returns bool
    /// </summary>
    /// <param name="userGuid">User Guid</param>
    /// <param name="roleId">Role Id to be removed</param>
    /// <returns></returns>
    public bool RemoveRoleFromUser(Guid userGuid, int roleId)
    {
        try
        {

            var result = _userRoleRepo.Delete(x => x.UserGuid == userGuid && x.RoleId == roleId);
            return result;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Sends classname and error message to ErrorLogger in Helper
    /// </summary>
    /// <param name="errorMessage">String value for logging of error</param>
    private void LogError(string errorMessage)
    {
        string className = this.ToString() ?? "Unknown class";
        _errorLogger.Logger(className, errorMessage);
    }
}
