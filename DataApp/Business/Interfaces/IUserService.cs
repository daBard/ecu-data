using Business.DTOs;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserAddDTO newUser);
        Task<UserDetailsDTO> GetUserDetailsAsync(Guid userDetailsId);
        Task<IEnumerable<UserListDTO>> GetUserListAsync();
        Task<bool> UpdateUserDetailsAsync(UserDetailsDTO updatedUser);
        Task<bool> DeleteUserAsync();

        void StoreUserId(Guid id);
        UserDetailsDTO GetStoredUserDetailsDTO();
        Guid GetStoredUserId();
        
    }
}