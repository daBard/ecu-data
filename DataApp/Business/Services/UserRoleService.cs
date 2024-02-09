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
    /// Takes Users new list of Roles and adds or removes in database as needed
    /// </summary>
    /// <param name="roleDTOs">IEnumerable of type RoleDTO</param>
    /// <param name="userGuid">User Guid</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> UpdateUserRolesAsync(IEnumerable<RoleDTO> roleDTOs, Guid userGuid)
    {
        try
        {
            var roles = await _roleRepo.GetAllAsync();

            foreach (RoleEntity role in roles)
            {
                if (roleDTOs.Any(x => x.Id == role.Id))
                {
                    if (await _userRoleRepo.ExistsAsync(x => x.RoleId == role.Id && x.UserGuid == userGuid) == null)
                        await AddRoleToUserAsync(userGuid, role.Id);
                }
                else
                {
                    if (await _userRoleRepo.ExistsAsync(x => x.RoleId == role.Id && x.UserGuid == userGuid) != null)
                        await RemoveRoleFromUserAsync(userGuid, role.Id);
                }
            }
            return true;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Adds a role to a user, returns bool
    /// </summary>
    /// <param name="userGuid">User Guid</param>
    /// <param name="roleId">Role Id to be added</param>
    /// <returns>True if successful, else false</returns>
    private async Task<bool> AddRoleToUserAsync(Guid userGuid, int roleId)
    {
        try
        {
            UserRoleEntity userRoleEntity = new UserRoleEntity()
            {
                UserGuid = userGuid,
                RoleId = roleId
            };
            if (await _userRoleRepo.CreateAsync(userRoleEntity) != null)
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
    public async Task<IEnumerable<RoleDTO>> GetUserRolesAsync(Guid guid)
    {
        try
        {
            var roles = await _roleRepo.GetAllAsync();
            var userRoleEntities = await _userRoleRepo.GetAllFromGuidAsync(x => x.UserGuid == guid);

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
    private async Task<bool> RemoveRoleFromUserAsync(Guid userGuid, int roleId)
    {
        try
        {

            var result = await _userRoleRepo.DeleteAsync(x => x.UserGuid == userGuid && x.RoleId == roleId);
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
