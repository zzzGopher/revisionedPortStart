using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IDataAccess
{
    public Task<IEnumerable<newUsers>> ReadFromDB();
    Task AddUser(string name, string city, string state);
    Task DeleteUser(int? Id);
}