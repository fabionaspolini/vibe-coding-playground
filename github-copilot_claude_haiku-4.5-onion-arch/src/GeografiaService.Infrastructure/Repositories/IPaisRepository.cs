using GeografiaService.Domain.Entities;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Interface para o repositório de Países.
/// </summary>
public interface IPaisRepository : IRepository<Pais, string>
{
    /// <summary>
    /// Obtém um país pelo seu nome.
    /// </summary>
    /// <param name="nome">Nome do país.</param>
    Task<Pais?> GetByNomeAsync(string nome, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém um país pelo seu código ISO3.
    /// </summary>
    /// <param name="codigoISO3">Código ISO3 do país.</param>
    Task<Pais?> GetByCodigoISO3Async(string codigoISO3, CancellationToken cancellationToken = default);
}

