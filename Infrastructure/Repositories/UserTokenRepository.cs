using Application.Common.Interfaces;
using Domain.Users;

namespace Infrastructure.Repositories;

public class UserTokenRepository : RepositoryBase<UserToken>, IUserTokenRepository
{
    public UserTokenRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserToken?> GetByUserIdAsync(Guid userId)
    {
        return _dbSet.SingleOrDefault(t => t.UserId == userId);
    }

    public async Task<UserToken?> GetByTokenAsync(string token)
    {
        return _dbSet.SingleOrDefault(t => t.Token == token);
    }
}