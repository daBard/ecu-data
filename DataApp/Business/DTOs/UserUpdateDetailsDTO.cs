using Business.Interfaces;

namespace Business.DTOs;

public class UserUpdateDetailsDTO : IUserUpdateDetailsDTO
{
    public Guid Guid { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
}
