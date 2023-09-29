using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using uTestAndForms.Models;

namespace uTestAndForms.Data;

public class DataAccess : IDataAccess
{



    private List<newUsers> _newUsersList;

    private readonly IgetConnection _getConnection;

    public DataAccess(IgetConnection getConnection)
    {
        _getConnection = getConnection;
    }


    public async Task<List<newUsers>> ReadFromDB()
    {
        await using var connection = await _getConnection.getConnections();
        try
        {
            await using DbCommand cmd = new SqlCommand("SELECT * FROM Users",connection);

            cmd.CommandType = CommandType.Text;

            connection.Open();


            await using (var rdr = cmd.ExecuteReader())
            {
                var newUsers = new List<newUsers>();

                while (rdr.Read())
                {



                    newUsers.Add(new newUsers()
                    {
                        Name = Convert.ToString(rdr["Name"]),
                        City = Convert.ToString(rdr["City"]),
                        Id = Convert.ToInt32(rdr["Id"]),
                        State = Convert.ToString(rdr["State"])
                    });
                    _newUsersList = newUsers;
                }
            }

            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _newUsersList;
    }

    public async Task AddUser(string name,string city,string state)
    {

        try
        {
            await using var connection = await _getConnection.getConnections();

            await using SqlCommand cmd = new SqlCommand("INSERT INTO Users (name, city, state) VALUES(@Name,@City,@State)",connection);

            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 80).Value = name;
            cmd.Parameters.Add("@City", SqlDbType.VarChar, 80).Value = city;
            cmd.Parameters.Add("@State", SqlDbType.VarChar, 80).Value = state;




            connection.Open();

            await cmd.ExecuteNonQueryAsync();

            connection.Close();

        }
        catch (Exception e)
        {
            Console.Write(e.Source,e.Message);
            throw e;
        }
    }

}