using Application.Common.Interfaces;
using Domain.Users;

namespace Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public User? GetUserByEmail(string email)
    {
        return _dbSet.SingleOrDefault(u => u.Email == email);
    }
}
