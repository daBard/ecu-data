namespace DataApp_WPF.Interfaces
{
    public interface IUserList
    {
        Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}