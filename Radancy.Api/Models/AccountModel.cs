namespace Radancy.Api.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
}