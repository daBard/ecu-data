using DataApp_WPF.Interfaces;

namespace DataApp_WPF.Models;

public class ListUser() : IListUser
{
    public Guid Id { get; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
