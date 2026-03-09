using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

/// <summary>
/// Serviço para operações de países.
/// </summary>
public class PaisService(IPaisRepository repository, IKafkaEventService kafkaEventService)
{
    private const string KafkaTopic = "geografia.pais";

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    public async Task<Pais> CreateAsync(CreatePaisRequest request, CancellationToken cancellationToken = default)
    {
        var pais = request.ToPais();
        var created = await repository.CreateAsync(pais, cancellationToken);
        kafkaEventService.PublishCreate(KafkaTopic, created.Id, created);
        return created;
    }

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    public async Task<Pais?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await repository.GetByIdAsync(id, cancellationToken);

    /// <summary>
    /// Lista todos os países.
    /// </summary>
    public async Task<IEnumerable<Pais>> ListAsync(CancellationToken cancellationToken = default) =>
        await repository.ListAsync(cancellationToken);

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    public async Task<Pais> UpdateAsync(string id, UpdatePaisRequest request, CancellationToken cancellationToken = default)
    {
        var pais = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"País com ID {id} não encontrado.");

        request.ApplyTo(pais);
        var updated = await repository.UpdateAsync(pais, cancellationToken);
        kafkaEventService.PublishUpdate(KafkaTopic, updated.Id, updated);
        return updated;
    }

    /// <summary>
    /// Remove logicamente um país (marca como inativo).
    /// </summary>
    public async Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var removed = await repository.RemoveAsync(id, cancellationToken);
        if (removed)
        {
            kafkaEventService.PublishDelete(KafkaTopic, id);
        }
        return removed;
    }
}
