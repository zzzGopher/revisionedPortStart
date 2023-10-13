using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DataAccessLibrary;

public class getConnection : IgetConnection
{
    private readonly ConnectionStrings _connectionStrings;

    public getConnection(IOptions<ConnectionStrings> connectionString)
    {
        _connectionStrings = connectionString.Value;
    }

    public async Task<SqlConnection> getConnections()
    {
        var connection = new SqlConnection(_connectionStrings.DefaultConnectionString);
        return connection;
    }
}