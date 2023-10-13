using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Models;

public class newUsers
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string City { get; set; }

    [StringLength(12)] public string State { get; set; }
}