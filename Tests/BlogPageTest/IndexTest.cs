using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using uTestAndForms.Pages.Blogs;

namespace Tests;

public class IndexTest
{
    private readonly Mock<IOptions<ConnectionStrings>> cnString = new();

    private readonly Mock<IDataAccess> dataAccessFake = new();

    private readonly Mock<ILogger<IndexForBlogs>> dataLogger = new();

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

        pageModel.nameParam = user;

        //act


        await pageModel.OnGet();

        var expected = from s in newUsers where s.Name.Contains(user) select s;

        var result = pageModel.newUsers;


        //assert

        Assert.Equal(expected, result);
    }

    [Fact]
    public async void Blogs_Action_Deletes_User()
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

        var usersModel = new newUsers();
        usersModel.Id = 1;

        var pageModel = new IndexForBlogs(cnString.Object, dataAccessFake.Object, dataLogger.Object);

        dataAccessFake.Setup(da => da.DeleteUser(usersModel.Id))
            .Returns(Task.FromResult(pageModel.newUsers = newUsers.Where(nu => nu.Id != usersModel.Id)));


        //act

        var expected = new[]
        {
            new newUsers
            {
                City = "toronto",
                State = "Canada",
                Id = 2,
                Name = "Pinocchio"
            }
        } as IEnumerable<newUsers>;

        var actual = pageModel.newUsers;

        await pageModel.OnPostDeleteUserWithId();


        //assert

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public async void OnPost_Request_Adds_Users_with_corresponding_model_attributes()
    {
        //arrange

        var userModel = new[]
        {
            new newUsers
            {
                City = "test",
                Id = 3,
                Name = "Poki",
                State = "Harlem"
            }
        };

        var newUsers = new List<newUsers>();


        var pageModel = new IndexForBlogs(cnString.Object, dataAccessFake.Object, dataLogger.Object);

        dataAccessFake.Setup(da => da.AddUser(userModel[0].Name, userModel[0].City, userModel[0].State))
            .Returns(Task.FromResult(pageModel.newUsers = new[]
            {
                new newUsers
                {
                    City = "test",
                    Id = 3,
                    Name = "Poki",
                    State = "Harlem"
                }
            }));

        //act

        await pageModel.OnPostAddUsers();

        var expected = (IEnumerable<newUsers>)userModel;

        var result = pageModel.newUsers;

        //assert

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async void OnPostRequest_Will_Throw_Exceptions()
    {
        var pageModel = new IndexForBlogs(cnString.Object, dataAccessFake.Object, dataLogger.Object);


        Assert.ThrowsAsync<Exception>(() => { return pageModel.OnPostAddUsers(); });
    }
}