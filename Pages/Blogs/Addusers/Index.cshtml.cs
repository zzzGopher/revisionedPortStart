using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using uTestAndForms.Data;
using uTestAndForms.Models;

namespace uTestAndForms.Pages.Blogs.Addusers;

public class Index : PageModel
{
    private IDataAccess _dataAccess;

    public Index(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    [BindProperty] public newUsers NewUsers { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAddUsers()
    {
        if (!ModelState.IsValid) return Page();

        await _dataAccess.AddUser(NewUsers.Name, NewUsers.City, NewUsers.State);
        return Redirect("/Blogs");
    }
}