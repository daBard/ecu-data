using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs;

public class ListUserDTO
{
    public Guid Id { get; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
