using Radancy.Api.Models;

namespace Radancy.Api.Services.Contracts;

public interface IAccountService
{
    Task<AccountModel> Create(Guid userId);
    Task<AccountModel> Withdraw(Guid accountId, decimal requestModelAmount);
    Task<AccountModel> Deposit(Guid accountId, decimal amount);
}