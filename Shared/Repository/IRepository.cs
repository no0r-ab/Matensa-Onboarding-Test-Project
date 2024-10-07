using Microsoft.EntityFrameworkCore.Query;
using SharedKernel.Repository;
using System.Linq.Expressions;
using System.Security.Cryptography;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAll();

    Task<T> Get(Guid id);

    Task<T> Add(T entity);

    Task<IEnumerable<T>> UpdateList (IEnumerable<T> entities);

    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);


}
