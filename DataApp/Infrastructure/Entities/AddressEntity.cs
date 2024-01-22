using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string? Street { get; set; }

    [Column(TypeName = "varchar(5)")]
    public int PostalCode { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? City { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}
