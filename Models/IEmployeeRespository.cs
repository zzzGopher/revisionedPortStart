namespace uTestAndForms.Models;

public interface IEmployeeRespository
{
    myUsers Add(myUsers myUsers);
    IEnumerable<myUsers> GetAllUsers();
    myUsers GetUser(int id);
    void ClearUser(int ID);
    void ClearAllUsers();


}