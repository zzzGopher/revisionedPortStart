using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using uTestAndForms.Data;
using uTestAndForms.Models;

namespace uTestAndForms.Pages.Blogs;

public class Index : PageModel
{

    public List<newUsers> newUsers;

    [BindProperty]
    public newUsers newUsersModel
    {
        get;
        set;
    }

    public IDataAccess _data;

    public IDeleteUser _deleteUser;

    public Index(IOptions<ConnectionStrings> cnStrings,IDataAccess data,IDeleteUser deleteUser)
    {
        _data = data;
        _deleteUser = deleteUser;
    }

    public async Task<IActionResult> OnGet()
    {
       var someData = await _data.ReadFromDB();
       newUsers = someData;
       return Page();
    }

    public async Task<IActionResult> OnPostDeleteUserWithId()
    {
       await _deleteUser.DeleteWithID(newUsersModel.Id);
       return Redirect("/Blogs");
    }




}