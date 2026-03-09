namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Interface base para repositórios genéricos.
/// Fornece operações básicas de CRUD.
/// </summary>
/// <typeparam name="TEntity">Tipo da entidade.</typeparam>
/// <typeparam name="TKey">Tipo da chave primária da entidade.</typeparam>
public interface IRepository<TEntity, TKey> where TEntity : class
{
    /// <summary>
    /// Obtém uma entidade por sua chave primária.
    /// </summary>
    /// <param name="id">A chave primária da entidade.</param>
    /// <returns>A entidade encontrada ou null se não existir.</returns>
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém todas as entidades.
    /// </summary>
    /// <returns>Coleção de todas as entidades.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém entidades usando um predicado de filtro.
    /// </summary>
    /// <param name="predicate">Função de predicado para filtrar entidades.</param>
    /// <returns>Coleção de entidades que correspondem ao predicado.</returns>
    Task<IEnumerable<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adiciona uma nova entidade ao repositório.
    /// </summary>
    /// <param name="entity">A entidade a ser adicionada.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza uma entidade no repositório.
    /// </summary>
    /// <param name="entity">A entidade a ser atualizada.</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove uma entidade do repositório.
    /// </summary>
    /// <param name="entity">A entidade a ser removida.</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}

