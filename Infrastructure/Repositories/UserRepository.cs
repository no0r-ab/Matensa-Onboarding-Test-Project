using Application.Common.Interfaces;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

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

    public List<User?> GetAll(bool includeTransactions = false)
    {
        if (includeTransactions)
            return _dbSet.Include(t => t.SentTransactions)
                         .Include(r => r.ReceivedTransactions).ToList();

        return _dbSet.ToList();
    }
}