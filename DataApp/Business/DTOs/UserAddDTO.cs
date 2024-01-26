namespace Business.DTOs
{
    public class UserAddDTO
    {
        public Guid Id { get; } = new Guid();
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
