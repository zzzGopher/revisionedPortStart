using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using uTestAndForms.Pages.Blogs;
using Index = uTestAndForms.Pages.Index;

namespace Tests;

public class IndexTest
{
    private readonly Mock<IOptions<ConnectionStrings>> cnString = new();

    private readonly Mock<IDataAccess> dataAccessFake = new();

    private readonly Mock<ILogger<Index>> dataLogger = new();

    [Fact]
    public async void Default_Blogs_Page_Assigns_new_users()
    {
        //arrange

        var newUsers = new List<newUsers>
        {
            new()
            {
                City = "atlanta", State = "georgia", Id = 1, Name = "wendy"
            }
        } as IEnumerable<newUsers>;

        dataAccessFake.Setup(d => d.ReadFromDB()).Returns(Task.FromResult(newUsers));


        //act

        var pageModel = new IndexForBlogs(cnString.Object, dataAccessFake.Object, dataLogger.Object);

        await pageModel.OnGet();

        var result = pageModel.newUsers;

        //assert

        Assert.Equal(newUsers, result);
    }

    [Theory]
    [InlineData("W")]
    [InlineData("P")]
    public async void Blogs_Page_with_parameters_finds_users(string user)
    {
        //arrange

        var newUsers = new List<newUsers>
        {
            new()
            {
                City = "atlanta", State = "georgia", Id = 1, Name = "Wendy"
            },
            new()
            {
                City = "toronto", State = "Canada", Id = 2, Name = "Pinocchio"
            }
        } as IEnumerable<newUsers>;

        dataAccessFake.Setup(d => d.ReadFromDB()).Returns(Task.FromResult(newUsers));

        var pageModel = new IndexForBlogs(cnString.Object, dataAccessFake.Object, dataLogger.Object);

        //act

        pageModel.nameParam = user;

        await pageModel.OnGet();

        var expected = from s in newUsers where s.Name.Contains(user) select s;

        foreach (var VARIABLE in expected)
        {
            Console.WriteLine(VARIABLE.Name);
        }

        var result = pageModel.newUsers;


        //assert

        Assert.Equal(expected, result);
    }
}