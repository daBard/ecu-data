using Business.DTOs;
using Business.Interfaces;
using Helper;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly ErrorLogger _errorLogger;
    private readonly UserRepo _userRepo;
    private readonly AddressRepo _addressRepo;
    private readonly UserProfileRepo _userProfileRepo;

    private Guid _userId;
    private UserDetailsDTO? _userDetailsDTO;

    public UserService(ErrorLogger errorLogger, UserRepo userRepo, AddressRepo addressRepo, UserProfileRepo userProfileRepo)
    {
        _errorLogger = errorLogger;
        _userRepo = userRepo;
        _addressRepo = addressRepo;
        _userProfileRepo = userProfileRepo;
    }

    /// <summary>
    /// Takes a UserAddDTO and creates a new user with UserEntity, AddressEntity and UserProfileEntity
    /// </summary>
    /// <param name="newUser">Takes an object of type UserAddDTO</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> CreateUserAsync(UserAddDTO newUser)
    {
        try
        {
            GenerateSecurePassword(newUser.Password, out string password, out string securityKey);

            UserEntity userEntity = new UserEntity();

            userEntity.Guid = newUser.Id;
            userEntity.UserName = newUser.UserName;
            userEntity.Password = password;
            userEntity.SecurityKey = securityKey;
            userEntity.Email = newUser.Email;

            userEntity.RegistrationDate = DateTime.Now;

            AddressEntity addressEntity = new AddressEntity();

            UserProfileEntity userProfileEntity = new UserProfileEntity();
            userProfileEntity.FirstName = newUser.FirstName;
            userProfileEntity.LastName = newUser.LastName;
            userProfileEntity.Birthdate = DateTime.Now;

            userEntity.Address = addressEntity;
            userEntity.UserProfile = userProfileEntity;

            if (await _userRepo.CreateAsync(userEntity) != null)
            {
                return true;
            }
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Gets all Users from Repo
    /// </summary>
    /// <returns>An IEnumerable of UserListDTOs</returns>
    public async Task<IEnumerable<UserListDTO>> GetUserListAsync()
    {
        List<UserListDTO> listUserDTOs = new List<UserListDTO>();

        var repoUsers = await _userRepo.GetAllAsync();

        foreach (var repoUser in repoUsers)
        {
            UserListDTO listUserDTO = new UserListDTO
            {
                Id = repoUser.Guid,
                UserName = repoUser.UserName,
                Email = repoUser.Email,
            };
            listUserDTOs.Add(listUserDTO);
        }

        return listUserDTOs;
    }

    /// <summary>
    /// Gets details for one User 
    /// </summary>
    /// <param name="userDetailsId">User Guid</param>
    /// <returns>A UserDetails DTO if successful, else null</returns>
    public async Task<UserDetailsDTO> GetUserDetailsAsync(Guid userDetailsId)
    {
        var userEntity = await _userRepo.GetOneAsync(x => (x.Guid == userDetailsId));
        if (userEntity != null)
        {
            var userProfileEntity = await _userProfileRepo.GetOneAsync(x => x.Id == userEntity.UserProfileId);
            var userAddressEntity = await _addressRepo.GetOneAsync(x => x.Id == userEntity.AddressId);

            UserDetailsDTO userDetailsDTO = new UserDetailsDTO
            {
                Guid = userEntity.Guid,
                FirstName = userProfileEntity.FirstName,
                LastName = userProfileEntity.LastName,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                RegistrationDate = userEntity.RegistrationDate,
                Street = userAddressEntity.Street,
                PostalCode = userAddressEntity.PostalCode,
                City = userAddressEntity.City
            };
            _userDetailsDTO = userDetailsDTO;
            return userDetailsDTO;
        }
        return null!;
    }

    /// <summary>
    /// Updates details for one User
    /// </summary>
    /// <param name="updatedUser">Object of type UserDetailsDTO</param>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> UpdateUserDetailsAsync(UserDetailsDTO updatedUser)
    {
        try
        {
            UserEntity updatedEntity = await _userRepo.GetOneAsync(x => x.Guid == _userId);

            updatedEntity.UserProfile.FirstName = updatedUser.FirstName!;
            updatedEntity.UserProfile.LastName = updatedUser.LastName!;
            updatedEntity.Address.Street = updatedUser.Street;
            updatedEntity.Address.PostalCode = updatedUser.PostalCode;
            updatedEntity.Address.City = updatedUser.City;

            var userEntity = await _userRepo.UpdateAsync(x => x.Guid == updatedEntity.Guid, updatedEntity);

            if (userEntity != null)
            {
                return true;
            }
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    /// <summary>
    /// Deletes User currently shown in UserDetailsView
    /// </summary>
    /// <returns>True if successful, else false</returns>
    public async Task<bool> DeleteUserAsync()
    {
        try
        {
            UserEntity userEntity = await _userRepo.GetOneAsync(x => x.Guid == _userId);
            var result = await _addressRepo.DeleteAsync(x => x.Id == userEntity.AddressId);
            result = await _userProfileRepo.DeleteAsync(x => x.Id == userEntity.UserProfileId);
            result = await _userRepo.DeleteAsync(x => x.Guid == _userId);
            return result;
        }
        catch (Exception ex) { LogError(ex.Message); }
        return false;
    }

    public void StoreUserId(Guid id) { _userId = id; }

    public Guid GetStoredUserId() { return _userId; }

    public UserDetailsDTO GetStoredUserDetailsDTO()
    {
        if (_userDetailsDTO != null)
            return _userDetailsDTO;
        else
            LogError("Stored UserDetailsDTO is null");
        return null!;
    }

    private void GenerateSecurePassword(string password, out string generatedPassword, out string securityKey)
    {
        using var hmac = new HMACSHA256();
        securityKey = Convert.ToBase64String(hmac.Key);
        generatedPassword = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
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
