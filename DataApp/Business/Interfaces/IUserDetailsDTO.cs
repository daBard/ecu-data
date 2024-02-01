namespace Business.Interfaces
{
    public interface IUserDetailsDTO
    {
        string? City { get; set; }
        string Email { get; set; }
        string? FirstName { get; set; }
        Guid Guid { get; set; }
        string? LastName { get; set; }
        string? PostalCode { get; set; }
        DateTime RegistrationDate { get; set; }
        string? Street { get; set; }
        string UserName { get; set; }
    }
}