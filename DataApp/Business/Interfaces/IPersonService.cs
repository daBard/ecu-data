using Business.DTOs;
using System.Collections.ObjectModel;

namespace Business.Interfaces
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