using uTestAndForms.Models;

namespace uTestAndForms.Data;

public interface IDataAccess
{
    Task<IEnumerable<newUsers>> ReadFromDB();
    Task AddUser(string name, string city, string state);
}