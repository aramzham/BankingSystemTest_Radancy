namespace Radancy.Api.Data;

public class User
{
    public Guid Id { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}