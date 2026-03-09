using GeografiaService.Domain.Entities;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Interface para o repositório de Estados.
/// </summary>
public interface IEstadoRepository : IRepository<Estado, string>
{
    /// <summary>
    /// Obtém todos os estados de um país.
    /// </summary>
    /// <param name="paisId">Identificador do país.</param>
    Task<IEnumerable<Estado>> GetByPaisIdAsync(string paisId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um estado pela sua sigla.
    /// </summary>
    /// <param name="sigla">Sigla do estado.</param>
    Task<Estado?> GetBySignatureAsync(string sigla, CancellationToken cancellationToken = default);
}

