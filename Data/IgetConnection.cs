using Microsoft.Data.SqlClient;

namespace uTestAndForms.Data;

public interface IgetConnection
{
    Task<SqlConnection> getConnections();
}