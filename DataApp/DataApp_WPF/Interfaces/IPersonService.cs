using DataApp_WPF.Models;
using System.Collections.ObjectModel;

namespace DataApp_WPF.Interfaces
{
    public interface IPersonService
    {
        bool AddPersonToList(Person person);

        ObservableCollection<Person> GetPersonList();
    }
}