using Radancy.Api.Data;

namespace Radancy.Api.Repositories.Contracts;

public interface IAccountRepository : IBaseRepository
{
    Task<Account> Create(Guid userId);
    Task<Account> Get(Guid accountId);
}