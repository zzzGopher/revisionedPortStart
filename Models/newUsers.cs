using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace uTestAndForms.Models;

public class newUsers
{
    [Key] public int Id { get; set; }

    [Required]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string Name { get; set; }

    [Required] [DisplayName("wee")] public string City { get; set; }

    [StringLength(12)] public string State { get; set; }
}