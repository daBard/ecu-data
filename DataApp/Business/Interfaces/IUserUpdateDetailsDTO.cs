namespace Business.Interfaces
{
    public interface IUserUpdateDetailsDTO
    {
        string? City { get; set; }
        string? FirstName { get; set; }
        Guid Guid { get; set; }
        string? LastName { get; set; }
        string? PostalCode { get; set; }
        string? Street { get; set; }
    }
}