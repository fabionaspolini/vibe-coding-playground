using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Países.
/// </summary>
public class PaisRepository : Repository<Pais, string>, IPaisRepository
{
    /// <summary>
    /// Inicializa uma nova instância do repositório de Países.
    /// </summary>
    public PaisRepository(GeografiaDbContext context) : base(context) { }

    /// <summary>
    /// Obtém um país pelo seu nome.
    /// </summary>
    public async Task<Pais?> GetByNomeAsync(string nome, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(p => p.Nome == nome, cancellationToken);

    /// <summary>
    /// Obtém um país pelo seu código ISO3.
    /// </summary>
    public async Task<Pais?> GetByCodigoISO3Async(string codigoISO3, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(p => p.CodigoISO3 == codigoISO3, cancellationToken);
}

