namespace DataApp_WPF.Interfaces
{
    public interface IUserDetailsModel
    {
        string? City { get; set; }
        string Email { get; set; }
        string? FirstName { get; set; }
        string? LastName { get; set; }
        string? PostalCode { get; set; }
        DateTime RegistrationDate { get; set; }
        string? Street { get; set; }
        string UserName { get; set; }
    }
}