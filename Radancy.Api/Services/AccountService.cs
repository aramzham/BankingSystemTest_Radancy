using Mapster;
using Radancy.Api.Models;
using Radancy.Api.Repositories.Contracts;
using Radancy.Api.Services.Contracts;

namespace Radancy.Api.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountModel> Create(int userId)
    {
        var account = await _accountRepository.Create(userId);
        return account.Adapt<AccountModel>();
    }

    public async Task<AccountModel> Withdraw(int accountId, decimal amount)
    {
        var account = await _accountRepository.Get(accountId);
        if(account is null)
            throw new Exception("Account not found.");
        
        if (amount < 0)
        {
            throw new InvalidOperationException("Withdrawal amount cannot be less than 0.");
        }
        
        // A user cannot withdraw more than 90% of their total balance from an account in a single transaction.
        if (amount > account.Balance * 0.9m)
        {
            throw new InvalidOperationException("Withdrawal amount is greater than 90% of the account balance.");
        }

        // An account cannot have less than $100 at any time in an account.
        if (account.Balance - amount < 100m)
        {
            throw new InvalidOperationException("Account balance cannot be less than 100.");
        }

        account.Balance -= amount;

        await _accountRepository.SaveChanges();

        return account.Adapt<AccountModel>();
    }

    public async Task<AccountModel> Deposit(int accountId, decimal amount)
    {
        var account = await _accountRepository.Get(accountId);
        if(account is null)
            throw new Exception("Account not found.");
        
        // A user cannot deposit more than $10,000 in a single transaction.
        if (amount > 10000m)
        {
            throw new InvalidOperationException("Deposit amount is greater than 10000.");
        }
        
        account.Balance += amount;
        
        await _accountRepository.SaveChanges();
        
        return account.Adapt<AccountModel>();
    }
}