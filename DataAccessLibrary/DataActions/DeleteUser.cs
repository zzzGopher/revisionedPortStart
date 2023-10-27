using System.Data;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary;

public class DeleteUser : IDeleteUser
{
    private readonly IgetConnection _getConnection;

    public DeleteUser(IgetConnection getConnection)
    {
        _getConnection = getConnection;
    }

    public async Task DeleteWithID(int? Id)
    {
        await using var connection = await _getConnection.getConnections();

        try
        {
            using (var cmd = new SqlCommand("DELETE FROM Users WHERE Users.Id = @Id", connection))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                connection.Open();

                await cmd.ExecuteNonQueryAsync();

                connection.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message, e.Source);
            throw;
        }
    }
}