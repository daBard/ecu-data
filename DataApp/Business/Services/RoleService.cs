using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class RoleService
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

    public bool Create(string roleName)
    {
        RoleEntity roleEntity = new RoleEntity() 
        {
            RoleName = roleName
        };

        var result = _roleRepo.Create(roleEntity);

        if (result != null)
            return true;

        return false;
    }

    public IEnumerable<RoleDTO> GetAll()
    {
        var roleEntities = _roleRepo.GetAll();
        List<RoleDTO> roleDTOs = new List<RoleDTO>();

        foreach (var entity in roleEntities)
        {
            RoleDTO dto = new RoleDTO()
            {
                Id = entity.Id,
                RoleName = entity.RoleName
            };
            roleDTOs.Add(dto);
        }
        
        return roleDTOs;
    }

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

    public RoleEntity Exists(string roleName)
    {
        try
        {
            var result = _roleRepo.Exists(x => x.RoleName == roleName);
            return result;
        }
        catch(Exception ex) { LogError(ex.Message); }
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
