using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using uTestAndForms.Data;
using uTestAndForms.Models;

namespace uTestAndForms.Pages.Blogs.Addusers;

public class Index : PageModel
{
    private IDataAccess _dataAccess;

    [BindProperty]
    public newUsers NewUsers {
        get;
        set;
    }

    public Index(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAddUsers()
    {
        await _dataAccess.AddUser(NewUsers.Name,NewUsers.City,NewUsers.State);
        return Redirect("/Blogs");
    }
}