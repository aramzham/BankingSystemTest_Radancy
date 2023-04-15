using Radancy.Api.Data;
using Radancy.Api.Repositories.Contracts;

namespace Radancy.Api.Repositories;

public class BaseRepository : IBaseRepository
{
    protected readonly RadancyDbContext _dbContext;

    public BaseRepository(RadancyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChanges()
    {
        return _dbContext.SaveChangesAsync();
    }
}