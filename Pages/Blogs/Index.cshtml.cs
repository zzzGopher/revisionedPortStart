using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace uTestAndForms.Pages.Blogs;

public class IndexForBlogs : PageModel
{
    public IDataAccess _data;

    public ILogger<Index> _logger;

    public IEnumerable<newUsers> newUsers;


    public IndexForBlogs(IOptions<ConnectionStrings> cnStrings, IDataAccess data, ILogger<Index> Logger)
    {
        _logger = Logger;
        _data = data;
    }

    [BindProperty] public newUsers newUsersModel { get; set; }


    [BindProperty(SupportsGet = true)] public string nameParam { get; set; }


    public async Task<IActionResult> OnGet()
    {
        var someData = await _data.ReadFromDB();

        if (!nameParam.IsNullOrEmpty())
        {
            _logger.LogInformation("loaded users");

            someData = someData.Where(d => d.Name.Contains(nameParam));
        }


        newUsers = someData;
        return Page();
    }

    public async Task<IActionResult> OnGetRefresh()
    {
        _logger.LogInformation("hitted");
        return Redirect("/Blogs");
    }

    public async Task<IActionResult> OnPostDeleteUserWithId()
    {
        await _data.DeleteUser(newUsersModel.Id);
        return Redirect("/Blogs");
    }

    public async Task<IActionResult> OnPostAddUsers()
    {
        try
        {
            await _data.AddUser(newUsersModel.Name, newUsersModel.City, newUsersModel.State);
            return Redirect("/Blogs");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}