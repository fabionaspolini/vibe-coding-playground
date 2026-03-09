using GeografiaService.Domain.Entities;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Interface para o repositório de Cidades.
/// </summary>
public interface ICidadeRepository : IRepository<Cidade, Guid>
{
    /// <summary>
    /// Obtém todas as cidades de um estado.
    /// </summary>
    /// <param name="estadoId">Identificador do estado.</param>
    Task<IEnumerable<Cidade>> GetByEstadoIdAsync(string estadoId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém uma cidade pelo seu nome.
    /// </summary>
    /// <param name="nome">Nome da cidade.</param>
    Task<Cidade?> GetByNomeAsync(string nome, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém uma cidade pelo seu código postal.
    /// </summary>
    /// <param name="codigoPostal">Código postal da cidade.</param>
    Task<Cidade?> GetByCodigoPostalAsync(string codigoPostal, CancellationToken cancellationToken = default);
}

