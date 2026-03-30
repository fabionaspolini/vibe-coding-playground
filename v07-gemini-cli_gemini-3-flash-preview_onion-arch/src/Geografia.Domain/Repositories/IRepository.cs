using Geografia.Domain.Entities;
using System.Linq.Expressions;

namespace Geografia.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(object id);
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task SaveChangesAsync();
}

public interface IPaisRepository : IRepository<Pais> { }
public interface IEstadoRepository : IRepository<Estado> { }
public interface ICidadeRepository : IRepository<Cidade> { }
