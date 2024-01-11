using Dapper;
using DataApp_WPF.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Controls;

namespace DataApp_WPF.Repositories;

internal class PersonRepo()
{
    private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\test\\testdb.mdf;Integrated Security=True;Connect Timeout=30";

    //CREATE
    public bool InsertNewPerson()
    {
        try
        {
            string sqlQuery = "INSERT INTO People(id, firstname, lastname, age, favouritefood) VALUES (@NewID, @NewFirstName, @NewLastName, @NewAge, @NewFavouriteFood);";
            return true;
        }
        catch(Exception ex) { Debug.WriteLine(ex); }
        return false;
    }

    public IEnumerable<PersonEntity> SelectAllPeople()
    {
        string sqlQuery = "SELECT id, firstname, lastname, age, favouritefood FROM People";
        using var conn = new SqlConnection(_connectionString);
        return conn.Query<PersonEntity>(sqlQuery);

    }

    //REPLACE

    //UPDATE

    //DELETE
}
