using Radancy.Api.Data;
using Radancy.Api.Repositories.Contracts;

namespace Radancy.Api.Repositories;

public class AccountRepository : BaseRepository, IAccountRepository
{
    public AccountRepository(RadancyDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<Account> Create(int userId)
    {
        var existingUser = await  _dbContext.Users.FindAsync(userId);
        if (existingUser is null)
            throw new Exception("User not found");
        
        var result = await _dbContext.Accounts.AddAsync(new Account()
        {
            UserId = userId,
            Balance = 100// An account cannot have less than $100 at any time in an account.
        });

        await  _dbContext.SaveChangesAsync();
        
        return result.Entity;
    }

    public async Task<Account> Get(int accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(accountId);
        if (account is null)
            throw new Exception("Account not found");

        return account;
    }
}