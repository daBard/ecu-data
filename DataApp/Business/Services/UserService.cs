using Business.DTOs;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Business.Services;

public class UserService
{
    private readonly ErrorLogger _errorLogger;
    private readonly UserRepo _userRepo;

    public UserService(ErrorLogger errorLogger, UserRepo userRepo)
    {
        _errorLogger = errorLogger;
        _userRepo = userRepo;
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

            AddressEntity addressEntity = new AddressEntity();
            //addressEntity.Id = 1;
            addressEntity.Street = "Gata";
            addressEntity.PostalCode = "12345";
            addressEntity.City = "By";

            UserProfileEntity userProfileEntity = new UserProfileEntity();
            //userProfileEntity.Id = 1;
            userProfileEntity.FirstName = "Test";
            userProfileEntity.LastName = "Testson";
            userProfileEntity.Birthdate = DateTime.Now;

            //userEntity.AddressId = 1; //TEMP
            userEntity.Address = addressEntity;
            //userEntity.RoleId = 1; //TEMP
            userEntity.UserProfile = userProfileEntity;


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
