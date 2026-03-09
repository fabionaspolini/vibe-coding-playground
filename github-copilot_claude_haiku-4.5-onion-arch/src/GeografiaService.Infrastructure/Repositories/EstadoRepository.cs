using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GeografiaService.Infrastructure.Repositories;

/// <summary>
/// Implementação do repositório de Estados.
/// </summary>
public class EstadoRepository : Repository<Estado, string>, IEstadoRepository
{
    /// <summary>
    /// Inicializa uma nova instância do repositório de Estados.
    /// </summary>
    public EstadoRepository(GeografiaDbContext context) : base(context) { }

    /// <summary>
    /// Obtém todos os estados de um país.
    /// </summary>
    public async Task<IEnumerable<Estado>> GetByPaisIdAsync(string paisId, CancellationToken cancellationToken = default) =>
        await DbSet.Where(e => e.PaisId == paisId).ToListAsync(cancellationToken);

    /// <summary>
    /// Obtém um estado pela sua sigla.
    /// </summary>
    public async Task<Estado?> GetBySignatureAsync(string sigla, CancellationToken cancellationToken = default) =>
        await DbSet.FirstOrDefaultAsync(e => e.Sigla == sigla, cancellationToken);
}

