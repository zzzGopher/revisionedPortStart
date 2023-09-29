using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace uTestAndForms.Data;

public class getConnection : IgetConnection
{
    private ConnectionStrings _connectionStrings;

    public getConnection(IOptions<ConnectionStrings> connectionString)
    {
        _connectionStrings = connectionString.Value;
    }

    public async Task<SqlConnection> getConnections()
    {
        SqlConnection connection = new SqlConnection(_connectionStrings.DefaultConnectionString);
        return connection;
    }
}