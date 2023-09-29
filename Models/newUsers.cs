using System.ComponentModel.DataAnnotations;

namespace uTestAndForms.Models;

public class newUsers
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}