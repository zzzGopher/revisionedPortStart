namespace DataAccessLibrary;

public interface IDeleteUser
{
    Task DeleteWithID(int? Id);
}