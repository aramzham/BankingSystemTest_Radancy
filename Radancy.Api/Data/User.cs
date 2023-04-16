namespace Radancy.Api.Data;

public class User
{
    public Guid Id { get; set; }
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}