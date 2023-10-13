namespace DataAccessLibrary;

public interface IAddUsers
{
    public Task Add(string name, string city, string state);
}