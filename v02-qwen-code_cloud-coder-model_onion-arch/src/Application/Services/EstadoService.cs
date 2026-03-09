using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

/// <summary>
/// Serviço para operações de estados.
/// </summary>
public class EstadoService(IEstadoRepository repository, IKafkaEventService kafkaEventService)
{
    private const string KafkaTopic = "geografia.estado";

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    public async Task<Estado> CreateAsync(CreateEstadoRequest request, CancellationToken cancellationToken = default)
    {
        var estado = request.ToEstado();
        var created = await repository.CreateAsync(estado, cancellationToken);
        kafkaEventService.PublishCreate(KafkaTopic, created.Id, created);
        return created;
    }

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    public async Task<Estado?> GetByIdAsync(string id, CancellationToken cancellationToken = default) =>
        await repository.GetByIdAsync(id, cancellationToken);

    /// <summary>
    /// Lista todos os estados.
    /// </summary>
    public async Task<IEnumerable<Estado>> ListAsync(CancellationToken cancellationToken = default) =>
        await repository.ListAsync(cancellationToken);

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    public async Task<Estado> UpdateAsync(string id, UpdateEstadoRequest request, CancellationToken cancellationToken = default)
    {
        var estado = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Estado com ID {id} não encontrado.");

        request.ApplyTo(estado);
        var updated = await repository.UpdateAsync(estado, cancellationToken);
        kafkaEventService.PublishUpdate(KafkaTopic, updated.Id, updated);
        return updated;
    }

    /// <summary>
    /// Remove logicamente um estado (marca como inativo).
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
