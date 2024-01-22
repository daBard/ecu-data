﻿using System.Collections.ObjectModel;
using System.Diagnostics;

using DataApp_WPF.Interfaces;
using DataApp_WPF.Models;
using DataApp_WPF.Repositories;

namespace DataApp_WPF.Services;

public class PersonService : IPersonService
{
    private ObservableCollection<Person> _personList = new();
    private Guid _storedId;

    public bool AddPersonToList(Person person)
    {
        try
        {
            _personList.Add(person);
            return true;
        }
        catch(Exception ex) { Debug.WriteLine(ex); }
        return false;
    }

    public bool UpdatePersonInList(Person person)
    {
        return true;
    }

    public bool DeletePersonFromList()
    {
        try
        {
            Person person = _personList.Single(x => x.Id == _storedId)!;
            _personList.Remove(person);
            return true;
        }
        catch(Exception ex) { Debug.WriteLine(ex);
        return false; }
    }

    public Person GetPerson()
    {
        try
        {
            return _personList.Single(x=>x.Id == _storedId);
        }
        catch (Exception e) { Debug.WriteLine(e); }
        return null!;
    }

    public ObservableCollection<Person> GetPersonList()
    { 
        return _personList;
    }

    public void StoreId(Guid id)
    {
        _storedId = id;
    }

    public Guid GetStoredId()
    { 
        return _storedId;
    }
}