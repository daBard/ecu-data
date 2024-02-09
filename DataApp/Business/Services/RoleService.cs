using Business.DTOs;
using Business.Interfaces;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class RoleService : IRoleService
{
    private readonly RoleRepo _roleRepo;
    private readonly UserRoleRepo _userRoleRepo;
    private readonly ErrorLogger _errorLogger;

    public RoleService(RoleRepo roleRepo, UserRoleRepo userRoleRepo, ErrorLogger errorLogger)
    {
        _roleRepo = roleRepo;
        _userRoleRepo = userRoleRepo;
        _errorLogger = errorLogger;
    }

    /// <summary>
    /// Creates a new role
    /// </summary>
    /// <param name="roleName">The role name as string</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> CreateAsync(string roleName)
    {
        try
        {
            RoleEntity roleEntity = new RoleEntity()
            {
                RoleName = roleName
            };

            var result = await _roleRepo.CreateAsync(roleEntity);

            if (result != null)
                return true;
        }
        catch (Exception ex) { LogError(ex.Message); }

        return false;
    }

    /// <summary>
    /// Gets a list of all roles
    /// </summary>
    /// <returns>An IEnumerable of type RoleDTO</returns>
    public async Task<IEnumerable<RoleDTO>> GetAllAsync()
    {
        List<RoleDTO> roleDTOs = new List<RoleDTO>();
        try
        {
            var roleEntities = await _roleRepo.GetAllAsync();

            foreach (var entity in roleEntities)
            {
                RoleDTO dto = new RoleDTO()
                {
                    Id = entity.Id,
                    RoleName = entity.RoleName
                };
                roleDTOs.Add(dto);
            }
        }
        catch (Exception ex) { LogError(ex.Message); }
        return roleDTOs;
    }

    /// <summary>
    /// Deletes a role
    /// </summary>
    /// <param name="id">Role id as int</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var userRoles = await _userRoleRepo.GetAllAsync();

            if (userRoles != null)
            {
                foreach (var userRole in userRoles)
                {
                    await _userRoleRepo.DeleteAsync(x => x.RoleId == id);
                }
            }

            await _roleRepo.DeleteAsync(x => x.Id == id);
            return true;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Takes a role name and checks if it already exists
    /// </summary>
    /// <param name="roleName">Role name as string</param>
    /// <returns>A RoleEntity if successful, else null</returns>
    public async Task<RoleEntity> ExistsAsync(string roleName)
    {
        try
        {
            var result = await _roleRepo.ExistsAsync(x => x.RoleName == roleName);
            return result;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return null!;
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
