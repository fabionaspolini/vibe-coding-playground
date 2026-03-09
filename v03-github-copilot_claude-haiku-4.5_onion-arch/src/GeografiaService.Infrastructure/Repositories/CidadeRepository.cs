using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Cidades.
/// </summary>
public class CidadeRepository : Repository<Cidade, Guid>, ICidadeRepository
{
    /// <summary>
    /// Inicializa uma nova instância do repositório de Cidades.
    /// </summary>
    public CidadeRepository(GeografiaDbContext context) : base(context) { }

    /// <summary>
    /// Obtém todas as cidades de um estado.
    /// </summary>
    public async Task<IEnumerable<Cidade>> GetByEstadoIdAsync(string estadoId, CancellationToken cancellationToken = default) =>
        await DbSet.Where(c => c.EstadoId == estadoId).ToListAsync(cancellationToken);

    /// <summary>
    /// Obtém uma cidade pelo seu nome.
    /// </summary>
    public async Task<Cidade?> GetByNomeAsync(string nome, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(c => c.Nome == nome, cancellationToken);

    /// <summary>
    /// Obtém uma cidade pelo seu código postal.
    /// </summary>
    public async Task<Cidade?> GetByCodigoPostalAsync(string codigoPostal, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(c => c.CodigoPostal == codigoPostal, cancellationToken);
}

