using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interface para repositório de cidades.
/// </summary>
public interface ICidadeRepository
{
    /// <summary>
    /// Adiciona uma nova cidade.
    /// </summary>
    Task<Cidade> CreateAsync(Cidade cidade, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    Task<Cidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todas as cidades.
    /// </summary>
    Task<IEnumerable<Cidade>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    Task<Cidade> UpdateAsync(Cidade cidade, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove logicamente uma cidade (marca como inativa).
    /// </summary>
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}
