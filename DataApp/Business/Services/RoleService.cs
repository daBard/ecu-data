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
    public bool Create(string roleName)
    {
        try
        {
            RoleEntity roleEntity = new RoleEntity()
            {
                RoleName = roleName
            };

            var result = _roleRepo.Create(roleEntity);

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
    public IEnumerable<RoleDTO> GetAll()
    {
        List<RoleDTO> roleDTOs = new List<RoleDTO>();
        try
        {
            var roleEntities = _roleRepo.GetAll();

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
    public bool Delete(int id)
    {
        try
        {
            var userRoles = _userRoleRepo.GetAll();

            if (userRoles != null)
            {
                foreach (var userRole in userRoles)
                {
                    _userRoleRepo.Delete(x => x.RoleId == id);
                }
            }

            _roleRepo.Delete(x => x.Id == id);
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
    public RoleEntity Exists(string roleName)
    {
        try
        {
            var result = _roleRepo.Exists(x => x.RoleName == roleName);
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
