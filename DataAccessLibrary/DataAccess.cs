using DataAccessLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLibrary;

public class DataAccess : IDataAccess, IGetAllUsers
{
    private readonly IAddUsers _addUser;

    private readonly IDeleteUser _deleteUser;

    private readonly IGetAllUsers _getAllUsers;

    private readonly IgetConnection _getConnection;

    private readonly ILogger<DataAccess> _logger;

    private readonly List<newUsers> _newUsersList = new();

    public DataAccess(IgetConnection getConnection, ILogger<DataAccess> logger, IGetAllUsers getAllUsers,
        IDeleteUser deleteUser, IAddUsers addUsers)
    {
        _getConnection = getConnection;

        _logger = logger;

        _getAllUsers = getAllUsers;

        _deleteUser = deleteUser;

        _addUser = addUsers;
    }


    public async Task<IEnumerable<newUsers>> ReadFromDB()
    {
        return await _getAllUsers.ReadFromDB();
    }

    public async Task AddUser(string name, string city, string state)
    {
        await _addUser.Add(name, city, state);
    }

    public async Task DeleteUser(int Id)
    {
        await _deleteUser.DeleteWithID(Id);
    }
}