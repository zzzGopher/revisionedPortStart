using DataAccessLibrary.Models;

namespace DataAccessLibrary;

public interface IGetAllUsers
{
    Task<IEnumerable<newUsers>> ReadFromDB();
}