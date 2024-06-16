using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonCrudApp.Models;

public class Person
{
    [Key, Column("id", Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, Column("first_name")]
    public required string FirstName { get; set; }

    [Required, Column("last_name")]
    public required string LastName { get; set; }

    [Required, Column("address")]
    public required string Address { get; set; }

    [Required, Column("tax_number")]
    public required string TaxNumber { get; set; }
}

