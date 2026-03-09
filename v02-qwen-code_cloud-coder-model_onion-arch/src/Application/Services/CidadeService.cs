using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

/// <summary>
/// Serviço para operações de cidades.
/// </summary>
public class CidadeService(ICidadeRepository repository, IKafkaEventService kafkaEventService)
{
    private const string KafkaTopic = "geografia.cidade";

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    public async Task<Cidade> CreateAsync(CreateCidadeRequest request, CancellationToken cancellationToken = default)
    {
        var cidade = request.ToCidade();
        var created = await repository.CreateAsync(cidade, cancellationToken);
        kafkaEventService.PublishCreate(KafkaTopic, created.Id.ToString(), created);
        return created;
    }

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    public async Task<Cidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await repository.GetByIdAsync(id, cancellationToken);

    /// <summary>
    /// Lista todas as cidades.
    /// </summary>
    public async Task<IEnumerable<Cidade>> ListAsync(CancellationToken cancellationToken = default) =>
        await repository.ListAsync(cancellationToken);

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    public async Task<Cidade> UpdateAsync(Guid id, UpdateCidadeRequest request, CancellationToken cancellationToken = default)
    {
        var cidade = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Cidade com ID {id} não encontrada.");

        request.ApplyTo(cidade);
        var updated = await repository.UpdateAsync(cidade, cancellationToken);
        kafkaEventService.PublishUpdate(KafkaTopic, updated.Id.ToString(), updated);
        return updated;
    }

    /// <summary>
    /// Remove logicamente uma cidade (marca como inativa).
    /// </summary>
    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var removed = await repository.RemoveAsync(id, cancellationToken);
        if (removed)
        {
            kafkaEventService.PublishDelete(KafkaTopic, id.ToString());
        }
        return removed;
    }
}
