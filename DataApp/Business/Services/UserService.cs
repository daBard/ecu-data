using Business.DTOs;
using Helper;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class UserService
{
    private readonly ErrorLogger _errorLogger;
    private readonly LocalDataContext _context;
    private readonly UserRepo _userRepo;

    public UserService(ErrorLogger errorLogger, LocalDataContext context)
    {
        _errorLogger = errorLogger;
        _context = context;
        _userRepo = new UserRepo(context);
    }

    public bool CreateUser()
    {
        try
        {
            UserEntity userEntity = new UserEntity();

            userEntity.Guid = Guid.NewGuid();
            userEntity.UserName = "JOCHEN";
            userEntity.Password = "BYTMIG123!";
            userEntity.Email = "jochen@example.com";

            //userEntity.UserName = newUser.UserName;
            //userEntity.Password = newUser.Password; //HASH PASSWORD
            //userEntity.Email = newUser.Email;

            userEntity.RegistrationDate = DateTime.Now;
            userEntity.AddressId = 0; //TEMP
            userEntity.RoleId = 0; //TEMP  

            _userRepo.Create(userEntity);
            
            return true;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    public bool DeleteUser()
    {
        return true;
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
