namespace Geografia.Infrastructure.Repositories;

using Geografia.Domain.Entities;
using Geografia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

/// <summary>
/// Interface genérica para repositórios.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retorna uma entidade pelo ID.
    /// </summary>
    /// <param name="id">ID da entidade.</param>
    /// <returns>A entidade encontrada ou null.</returns>
    Task<TEntity?> GetByIdAsync(object id);

    /// <summary>
    /// Retorna todas as entidades com filtros opcionais.
    /// </summary>
    /// <param name="filters">Dicionário com atributos e valores para filtrar.</param>
    /// <returns>Lista de entidades filtradas.</returns>
    Task<List<TEntity>> ListAsync(Dictionary<string, object>? filters = null);

    /// <summary>
    /// Adiciona uma nova entidade.
    /// </summary>
    /// <param name="entity">Entidade a ser adicionada.</param>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Atualiza uma entidade existente.
    /// </summary>
    /// <param name="entity">Entidade a ser atualizada.</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Salva as alterações no banco de dados.
    /// </summary>
    Task SaveChangesAsync();
}

/// <summary>
/// Implementação genérica do repositório.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly GeografiaDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Construtor do repositório.
    /// </summary>
    /// <param name="context">Contexto do banco de dados.</param>
    public Repository(GeografiaDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Retorna uma entidade pelo ID.
    /// </summary>
    /// <param name="id">ID da entidade.</param>
    /// <returns>A entidade encontrada ou null.</returns>
    public async Task<TEntity?> GetByIdAsync(object id) => 
        await _dbSet.FindAsync(id);

    /// <summary>
    /// Retorna todas as entidades com filtros opcionais.
    /// </summary>
    /// <param name="filters">Dicionário com atributos e valores para filtrar.</param>
    /// <returns>Lista de entidades filtradas.</returns>
    public async Task<List<TEntity>> ListAsync(Dictionary<string, object>? filters = null)
    {
        var query = _dbSet.AsQueryable();

        if (filters != null && filters.Count > 0)
        {
            query = ApplyFilters(query, filters);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Adiciona uma nova entidade.
    /// </summary>
    /// <param name="entity">Entidade a ser adicionada.</param>
    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Atualiza uma entidade existente.
    /// </summary>
    /// <param name="entity">Entidade a ser atualizada.</param>
    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Salva as alterações no banco de dados.
    /// </summary>
    public async Task SaveChangesAsync() => 
        await _context.SaveChangesAsync();

    /// <summary>
    /// Aplica filtros dinâmicos baseados em refle​ção.
    /// </summary>
    /// <param name="query">Query LINQ.</param>
    /// <param name="filters">Dicionário com atributos e valores para filtrar.</param>
    /// <returns>Query filtrada.</returns>
    private static IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, Dictionary<string, object> filters)
    {
        foreach (var filter in filters)
        {
            var property = typeof(TEntity).GetProperty(filter.Key, 
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                continue;

            var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity), "x");
            var propertyAccess = System.Linq.Expressions.Expression.Property(parameter, property);
            var constant = System.Linq.Expressions.Expression.Constant(filter.Value);
            var equality = System.Linq.Expressions.Expression.Equal(propertyAccess, constant);
            var lambda = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

            query = query.Where(lambda);
        }

        return query;
    }
}

