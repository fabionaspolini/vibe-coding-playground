using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repositório para operações de estados.
/// </summary>
public class EstadoRepository(GeografiaDbContext context) : IEstadoRepository
{
    /// <inheritdoc />
    public async Task<Estado> CreateAsync(Estado estado, CancellationToken cancellationToken = default)
    {
        await context.Estados.AddAsync(estado, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return estado;
    }

    /// <inheritdoc />
    public async Task<Estado?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await context.Estados.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<Estado>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.Estados.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Estado> UpdateAsync(Estado estado, CancellationToken cancellationToken = default)
    {
        context.Estados.Update(estado);
        await context.SaveChangesAsync(cancellationToken);
        return estado;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var estado = await context.Estados.FindAsync([id], cancellationToken);
        if (estado is null)
        {
            return false;
        }

        estado.Ativo = false;
        context.Estados.Update(estado);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
