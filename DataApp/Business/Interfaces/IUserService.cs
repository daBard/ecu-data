using Business.DTOs;

namespace Business.Interfaces
{
    public interface IUserService
    {
        bool CreateUser(UserAddDTO newUser);
        bool DeleteUser();
        UserDetailsDTO GetStoredUserDetailsDTO();
        Guid GetStoredUserId();
        UserDetailsDTO GetUserDetails(Guid userDetailsId);
        IEnumerable<UserListDTO> GetUserList();
        void StoreUserId(Guid id);
        bool UpdateUserDetails(UserDetailsDTO updatedUser);
    }
}