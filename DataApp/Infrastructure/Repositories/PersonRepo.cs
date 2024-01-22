using Dapper;
using DataApp_WPF.Entities;
using DataApp_WPF.Interfaces;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Text;

namespace DataApp_WPF.Repositories;

internal class PersonRepo(string connectionString) : IPersonRepo
{
    private readonly string _connectionString = connectionString;

   /// <summary>
   /// Saves a new person in database.
   /// </summary>
   /// <param name="entity">Takes type PersonEntity</param>
   /// <returns>Returns true if successful, else false.</returns>
    public bool InsertNewPerson(PersonEntity entity)
    {
        string sqlQuery = "INSERT INTO People(id, firstname, lastname, age, favouritefood) VALUES (@Id, @FirstName, @LastName, @Age, @FavouriteFood);";
        try
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using var cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Guid);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName);
                cmd.Parameters.AddWithValue("@LastName", entity.LastName);
                cmd.Parameters.AddWithValue("@Age", entity.Age);
                cmd.Parameters.AddWithValue("@FavouriteFood", entity.FavouriteFood);


            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex); }
        return false;
    }

    /// <summary>
    /// Gets all people from database.
    /// </summary>
    /// <returns>Returns an IEnumerable of type PersonEntity</returns>
    public IEnumerable<PersonEntity> SelectAllPeople()
    {
        string sqlQuery = "SELECT id, firstname, lastname, age, favouritefood FROM People";

        try
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using var cmd = new SqlCommand(sqlQuery, conn);



                //return conn.Query<PersonEntity>(sqlQuery);
            }
        }
        catch(Exception ex) { Debug.WriteLine(ex); }
        return new List<PersonEntity>();
    }

    //READ

    //UPDATE

    //DELETE
}
