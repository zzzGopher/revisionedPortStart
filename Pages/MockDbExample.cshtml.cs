using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using uTestAndForms.Data;
using uTestAndForms.Models;

namespace uTestAndForms.Pages;

public class IndexModel : PageModel
{


    public ILogger<IndexModel> _logger;


    [Required(ErrorMessage = "please select a gendyyy")]

    public string gender { get; set; }

    [BindProperty]
    public int _id {
        get;
        set;
    }

    public IEmployeeRespository _employeeRepo;


    public IndexModel(ILogger<IndexModel> logger,IEmployeeRespository employeeRepo)
    {
        _logger = logger;
        _employeeRepo = employeeRepo;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPost(myUsers myUsers)
    {
        _employeeRepo.Add(myUsers);
        _logger.LogInformation("posted");
        return Redirect("/Index");
    }

    public IActionResult OnPostDeleteUsers(int ID)
    {
        _logger.LogInformation("Deleting");
        _employeeRepo.ClearUser(ID);
        return Redirect("/Index");
    }

    public IActionResult OnPostDeleteAllUsers()
    {
        _employeeRepo.ClearAllUsers();
        return Redirect("/Index");
    }

}