using Domain.Users;

namespace Application.Common.Interfaces;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByEmail(string email);
    List<User?> GetAll(bool includeTransactions = false);

}
