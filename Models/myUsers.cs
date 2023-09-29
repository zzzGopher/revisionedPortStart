using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace uTestAndForms.Models;

public class myUsers
{
    [Key]

    public int id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    [Required]
    public string gender { get; set; }
}