using System.ComponentModel.DataAnnotations.Schema;

namespace Radancy.Api.Data;

public class Account
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public required int UserId { get; set; }
    public required decimal Balance { get; set; }

    public virtual User? AccountHolder { get; set; }
    // add other properties
}