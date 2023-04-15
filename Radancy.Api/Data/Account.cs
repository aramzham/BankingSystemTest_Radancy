namespace Radancy.Api.Data;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }

    public User AccountHolder { get; set; }
    // add other properties
}