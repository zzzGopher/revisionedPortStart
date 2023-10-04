using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using uTestAndForms.Data;
using uTestAndForms.Models;

namespace uTestAndForms.Pages.Blogs;

public class Index : PageModel
{
    public IDataAccess _data;

    public IDeleteUser _deleteUser;

    public ILogger<Index> _logger;

    public IEnumerable<newUsers> newUsers;


    public Index(IOptions<ConnectionStrings> cnStrings, IDataAccess data, IDeleteUser deleteUser, ILogger<Index> Logger)
    {
        _logger = Logger;
        _data = data;
        _deleteUser = deleteUser;
    }

    [BindProperty] public newUsers newUsersModel { get; set; }


    [BindProperty(SupportsGet = true)] public string nameParam { get; set; }


    public async Task<IActionResult> OnGet()
    {
        var someData = await _data.ReadFromDB();

        if (!nameParam.IsNullOrEmpty())
        {
            _logger.LogInformation("working");

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
        await _deleteUser.DeleteWithID(newUsersModel.Id);
        return Redirect("/Blogs");
    }
}