using DataApp_WPF.Interfaces;
using DataApp_WPF.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DataApp_WPF.Services
{
    public class PersonService : IPersonService
    {
        private ObservableCollection<Person> personList = [];

        public bool AddPersonToList(Person person)
        {
            try
            {
                personList.Add(person);
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
            return false;
        }

        public ObservableCollection<Person> GetPersonList() 
        { 
            return personList;
        }
    }
}
