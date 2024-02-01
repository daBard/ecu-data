using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IAddressEntity
    {
        string? City { get; set; }
        int Id { get; set; }
        string? PostalCode { get; set; }
        string? Street { get; set; }
        UserEntity User { get; set; }
    }
}