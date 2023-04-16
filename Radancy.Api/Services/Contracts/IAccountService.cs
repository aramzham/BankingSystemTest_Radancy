using Radancy.Api.Models;

namespace Radancy.Api.Services.Contracts;

public interface IAccountService
{
    Task<AccountModel> Create(int userId);
    Task<AccountModel> Withdraw(int accountId, decimal requestModelAmount);
    Task<AccountModel> Deposit(int accountId, decimal amount);
}