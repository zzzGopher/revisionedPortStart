using DataAccessLibrary;
using Moq;

namespace Tests;

public class Data
{
    [Fact]
    public void Test1()
    {
        var mockAccess = new Mock<IDataAccess>();

        mockAccess.Setup(x => x.ReadFromDB());
    }
}