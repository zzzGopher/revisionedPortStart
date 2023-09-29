namespace uTestAndForms.Models;

public class MockUserRepository:IEmployeeRespository
{
    private readonly List<myUsers> _users = new List<myUsers>() { new myUsers() { id = 1,firstName = "Timmy",lastName = "Simmons",gender = "Male"} };

    public myUsers Add(myUsers myUsers)
    {
        myUsers.id = !_users.Any() ? 1 : _users.Max(e => e.id + 1);

        _users.Add(myUsers);
        return myUsers;
    }

    public IEnumerable<myUsers> GetAllUsers()
    {
        return _users;
    }

    public myUsers GetUser(int id)
    {
        foreach (var user in _users)
        {
            if (user.id == id)
            {
                return user;
            }
        }

        return new myUsers();
    }

    public void ClearUser(int ID)
    {
        myUsers myUsersToRemove = _users.Find(pred => pred.id == ID);
        _users.Remove(myUsersToRemove);
    }

    public void ClearAllUsers()
    {
        _users.Clear();
    }
}