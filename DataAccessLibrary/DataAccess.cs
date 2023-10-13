using DataAccessLibrary.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLibrary;

public class DataAccess : IDataAccess, IGetAllUsers
{
    private readonly AddUser _addUser;
    private readonly DeleteUser _deleteUser;

    private readonly GetAllUsers _getAllUsers;

    private readonly IgetConnection _getConnection;

    private readonly ILogger<DataAccess> _logger;

    private readonly List<newUsers> _newUsersList = new();

    public DataAccess(IgetConnection getConnection, ILogger<DataAccess> logger)
    {
        _getConnection = getConnection;
        _logger = logger;

        _getAllUsers = new GetAllUsers(_newUsersList, _getConnection);

        _deleteUser = new DeleteUser(_getConnection);

        _addUser = new AddUser(_getConnection, _logger);
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