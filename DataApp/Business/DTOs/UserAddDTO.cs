using Business.Interfaces;

namespace Business.DTOs
{
    public class UserAddDTO : IUserAddDTO
    {
        public Guid Id { get; } = new Guid();
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        
    }
}
