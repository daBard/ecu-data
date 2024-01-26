using DataApp_WPF.Interfaces;

namespace DataApp_WPF.Models;

public class UserListModel() : IUserList
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
