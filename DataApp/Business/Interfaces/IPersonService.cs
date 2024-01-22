using DataApp_WPF.Models;
using System.Collections.ObjectModel;

namespace DataApp_WPF.Interfaces
{
    public interface IPersonService
    {
        bool AddPersonToList(Person person);
        bool UpdatePersonInList(Person person);
        bool DeletePersonFromList();
        Person GetPerson();
        ObservableCollection<Person> GetPersonList();
        void StoreId(Guid id);
        Guid GetStoredId();
    }
}