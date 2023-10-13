using System.Data;
using System.Data.Common;
using DataAccessLibrary.Models;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary;

public class GetAllUsers : IGetAllUsers
{
    private readonly IgetConnection _connection;

    private List<newUsers> _newUsersList;

    public GetAllUsers(List<newUsers> newUsersList, IgetConnection connection)
    {
        _newUsersList = newUsersList;
        _connection = connection;
    }


    public async Task<IEnumerable<newUsers>> ReadFromDB()
    {
        await using var connection = await _connection.getConnections();
        try
        {
            await using DbCommand cmd = new SqlCommand("SELECT * FROM Users", connection);

            cmd.CommandType = CommandType.Text;

            connection.Open();


            await using (var rdr = cmd.ExecuteReader())
            {
                var newUsers = new List<newUsers>();

                while (rdr.Read())
                {
                    newUsers.Add(new newUsers
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
}