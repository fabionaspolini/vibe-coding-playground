using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interface para repositório de países.
/// </summary>
public interface IPaisRepository
{
    /// <summary>
    /// Adiciona um novo país.
    /// </summary>
    Task<Pais> CreateAsync(Pais pais, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    Task<Pais?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todos os países.
    /// </summary>
    Task<IEnumerable<Pais>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    Task<Pais> UpdateAsync(Pais pais, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove logicamente um país (marca como inativo).
    /// </summary>
    Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default);
}
