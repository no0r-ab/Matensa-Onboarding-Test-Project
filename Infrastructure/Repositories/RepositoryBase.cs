using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryBase<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Get(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IQueryable<T> usersQuery = _dbSet;

        return await usersQuery.ToListAsync();
    }

    public async Task<IEnumerable<T>> UpdateList(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null || entity.DeletedAt.HasValue)
            return null;


        _dbSet.Attach(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        int? pageNumber, int? pageSize,
        CancellationToken cancellationToken = default)
    {
        var queryable = _dbSet.AsQueryable();

        queryable = queryable.Where(e => !e.DeletedAt.HasValue);

        if (pageNumber.HasValue && pageSize.HasValue)
            queryable = queryable.Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);

        var entities = await queryable.ToListAsync(cancellationToken);
        /*
                if (typeof(TranslatableEntity<>).MakeGenericType(typeof(T))
                    .IsAssignableFrom(typeof(T)))
                {
                    foreach (var entity in entities.Cast<TranslatableEntity<T>>())
                    {
                        // Use the Entry method to get an entry for the entity, and then call the Collection method to mark the Translations property as needing to be loaded
                        _context.Entry(entity).Collection(e => e.Translations).Load();
                    }
                }
        */
        return entities;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity.GetType() == typeof(Entity))
            (entity as Entity).SetUpdateDateTime();

        entity.SetUpdateDateTime();

        _context.Entry(entity).State = EntityState.Modified;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}