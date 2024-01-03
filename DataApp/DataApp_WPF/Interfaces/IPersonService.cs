using DataApp_WPF.Models;

namespace DataApp_WPF.Interfaces
{
    internal interface IPersonService
    {
        bool AddPerson();
        bool DeletePerson();
        bool UpdatePerson();
        IEnumerable<Person> GetPersonList();
        Person GetPersonDetails(Guid id);
    }
}
