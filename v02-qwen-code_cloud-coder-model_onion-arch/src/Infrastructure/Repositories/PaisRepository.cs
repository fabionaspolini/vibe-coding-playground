using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repositório para operações de países.
/// </summary>
public class PaisRepository(GeografiaDbContext context) : IPaisRepository
{
    /// <inheritdoc />
    public async Task<Pais> CreateAsync(Pais pais, CancellationToken cancellationToken = default)
    {
        await context.Paises.AddAsync(pais, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return pais;
    }

    /// <inheritdoc />
    public async Task<Pais?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await context.Paises.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<Pais>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.Paises.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Pais> UpdateAsync(Pais pais, CancellationToken cancellationToken = default)
    {
        context.Paises.Update(pais);
        await context.SaveChangesAsync(cancellationToken);
        return pais;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var pais = await context.Paises.FindAsync([id], cancellationToken);
        if (pais is null)
        {
            return false;
        }

        pais.Ativo = false;
        context.Paises.Update(pais);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
