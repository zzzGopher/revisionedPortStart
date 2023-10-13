using Microsoft.Data.SqlClient;

namespace DataAccessLibrary;

public interface IgetConnection
{
    Task<SqlConnection> getConnections();
}