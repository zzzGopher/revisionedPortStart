using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary;

public class AddUser : IAddUsers
{
    private readonly IgetConnection _connection;


    public AddUser(IgetConnection connection)
    {
        _connection = connection;
    }

    public async Task Add(string name, string city, string state)
    {
        try
        {
            await using var connection = await _connection.getConnections();

            await using var cmd = new SqlCommand("INSERT INTO Users (name, city, state) VALUES(@Name,@City,@State)",
                connection);

            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 80).Value = name;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 80).Value = city;
            cmd.Parameters.Add("@State", SqlDbType.VarChar, 80).Value = state;


            connection.Open();

            await cmd.ExecuteNonQueryAsync();

            connection.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}