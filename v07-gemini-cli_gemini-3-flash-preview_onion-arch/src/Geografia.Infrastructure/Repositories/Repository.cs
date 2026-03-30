using Geografia.Domain.Entities;
using Geografia.Domain.Repositories;
using Geografia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Geografia.Infrastructure.Repositories;

public class Repository<T>(GeografiaDbContext context) : IRepository<T> where T : class
{
    protected readonly GeografiaDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null) query = query.Where(filter);
        return await query.ToListAsync();
    }

    public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public virtual Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

public class PaisRepository(GeografiaDbContext context) : Repository<Pais>(context), IPaisRepository { }
public class EstadoRepository(GeografiaDbContext context) : Repository<Estado>(context), IEstadoRepository { }
public class CidadeRepository(GeografiaDbContext context) : Repository<Cidade>(context), ICidadeRepository { }
