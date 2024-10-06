using Domain.Users;

namespace Application.Common.Interfaces;

public interface IUserTokenRepository : IRepository<UserToken>
{
    Task<UserToken?> GetByUserIdAsync(Guid userId);
    Task<UserToken?> GetByTokenAsync(string token);
}
