using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

/// <summary>
/// Repositório para operações de cidades.
/// </summary>
public class CidadeRepository(GeografiaDbContext context) : ICidadeRepository
{
    /// <inheritdoc />
    public async Task<Cidade> CreateAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        await context.Cidades.AddAsync(cidade, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return cidade;
    }

    /// <inheritdoc />
    public async Task<Cidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context.Cidades.FindAsync([id], cancellationToken);

    /// <inheritdoc />
    public async Task<IEnumerable<Cidade>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.Cidades.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<Cidade> UpdateAsync(Cidade cidade, CancellationToken cancellationToken = default)
    {
        context.Cidades.Update(cidade);
        await context.SaveChangesAsync(cancellationToken);
        return cidade;
    }

    /// <inheritdoc />
    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cidade = await context.Cidades.FindAsync([id], cancellationToken);
        if (cidade is null)
        {
            return false;
        }

        cidade.Ativo = false;
        context.Cidades.Update(cidade);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
