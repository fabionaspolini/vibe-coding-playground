using GeografiaService.Application.DTOs;
using GeografiaService.Application.Extensions;
using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace GeografiaService.Application.Services;

/// <summary>
/// Serviço de negócios para gerenciar Estados.
/// </summary>
public class EstadoService
{
    private readonly IEstadoRepository _repository;
    private readonly IEventProducer _eventProducer;
    private readonly ILogger<EstadoService> _logger;

    /// <summary>
    /// Inicializa uma nova instância do serviço de Estado.
    /// </summary>
    public EstadoService(IEstadoRepository repository, IEventProducer eventProducer, ILogger<EstadoService> logger)
    {
        _repository = repository;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    public async Task<EstadoResponse> CreateAsync(CreateEstadoRequest request, CancellationToken cancellationToken = default)
    {
        var estado = request.ToEstado();
        await _repository.AddAsync(estado, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.estado", estado.Id, estado.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Estado criado: {estado.Id}");

        return estado.ToEstadoResponse();
    }

    /// <summary>
    /// Obtém um estado por seu ID.
    /// </summary>
    public async Task<EstadoResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var estado = await _repository.GetByIdAsync(id, cancellationToken);
        return estado?.ToEstadoResponse();
    }

    /// <summary>
    /// Lista todos os estados com filtros opcionais.
    /// </summary>
    public async Task<IEnumerable<EstadoResponse>> ListAsync(string? paisId = null, string? nome = null, bool? ativo = null, CancellationToken cancellationToken = default)
    {
        var estados = await _repository.GetAllAsync(cancellationToken);

        var query = estados.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(paisId))
            query = query.Where(e => e.PaisId == paisId);

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(e => e.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (ativo.HasValue)
            query = query.Where(e => e.Ativo == ativo.Value);

        return query.Select(e => e.ToEstadoResponse());
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    public async Task<EstadoResponse?> UpdateAsync(string id, UpdateEstadoRequest request, CancellationToken cancellationToken = default)
    {
        var estado = await _repository.GetByIdAsync(id, cancellationToken);
        if (estado is null)
            return null;

        estado.UpdateFromRequest(request);
        await _repository.UpdateAsync(estado, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.estado", estado.Id, estado.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Estado atualizado: {estado.Id}");

        return estado.ToEstadoResponse();
    }

    /// <summary>
    /// Remove um estado (soft-delete).
    /// </summary>
    public async Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var estado = await _repository.GetByIdAsync(id, cancellationToken);
        if (estado is null)
            return false;

        estado.Ativo = false;
        await _repository.UpdateAsync(estado, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.estado", estado.Id, estado.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Estado removido: {estado.Id}");

        return true;
    }
}

