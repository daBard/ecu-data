namespace DataApp_WPF.Interfaces
{
    public interface IListUser
    {
        Guid Id { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}