using GeografiaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Implementação genérica de repositório com operações básicas de CRUD.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
/// <typeparam name="TKey">Tipo da chave primária da entidade.</typeparam>
public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected readonly GeografiaDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    /// <summary>
    /// Inicializa uma nova instância do repositório.
    /// </summary>
    /// <param name="context">O contexto de banco de dados.</param>
    protected Repository(GeografiaDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Obtém uma entidade por sua chave primária.
    /// </summary>
    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default) =>
        await DbSet.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

    /// <summary>
    /// Obtém todas as entidades.
    /// </summary>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await DbSet.ToListAsync(cancellationToken);

    /// <summary>
    /// Obtém entidades usando um predicado de filtro.
    /// </summary>
    public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        return DbSet.Where(predicate).ToList();
    }

    /// <summary>
    /// Adiciona uma nova entidade ao repositório.
    /// </summary>
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Atualiza uma entidade no repositório.
    /// </summary>
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Remove uma entidade do repositório.
    /// </summary>
    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}

