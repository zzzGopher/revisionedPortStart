using Microsoft.Data.SqlClient;
using uTestAndForms.Models;

namespace uTestAndForms.Data;

public interface IDataAccess
{
    Task<List<newUsers>> ReadFromDB();
     Task AddUser(string name,string city,string state);
}