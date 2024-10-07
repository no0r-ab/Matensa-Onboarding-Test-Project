using Domain.Transactions;

namespace Application.Common.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction?>> AddList(IEnumerable<Transaction> entities);
}
