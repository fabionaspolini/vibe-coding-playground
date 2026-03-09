using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Interface para repositório de estados.
/// </summary>
public interface IEstadoRepository
{
    /// <summary>
    /// Adiciona um novo estado.
    /// </summary>
    Task<Estado> CreateAsync(Estado estado, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    Task<Estado?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todos os estados.
    /// </summary>
    Task<IEnumerable<Estado>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    Task<Estado> UpdateAsync(Estado estado, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove logicamente um estado (marca como inativo).
    /// </summary>
    Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default);
}
