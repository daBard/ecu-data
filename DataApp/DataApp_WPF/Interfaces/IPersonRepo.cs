using DataApp_WPF.Entities;

namespace DataApp_WPF.Interfaces
{
    internal interface IPersonRepo
    {
        bool InsertNewPerson(PersonEntity entity);
        IEnumerable<PersonEntity> SelectAllPeople();
    }
}