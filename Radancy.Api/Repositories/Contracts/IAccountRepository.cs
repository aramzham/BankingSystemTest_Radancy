using Radancy.Api.Data;

namespace Radancy.Api.Repositories.Contracts;

public interface IAccountRepository : IBaseRepository
{
    Task<Account> Create(int userId);
    Task<Account> Get(int accountId);
}