using GeografiaService.Application.DTOs;
using GeografiaService.Application.Extensions;
using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace GeografiaService.Application.Services;

/// <summary>
/// Serviço de negócios para gerenciar Cidades.
/// </summary>
public class CidadeService
{
    private readonly ICidadeRepository _repository;
    private readonly IEventProducer _eventProducer;
    private readonly ILogger<CidadeService> _logger;

    /// <summary>
    /// Inicializa uma nova instância do serviço de Cidade.
    /// </summary>
    public CidadeService(ICidadeRepository repository, IEventProducer eventProducer, ILogger<CidadeService> logger)
    {
        _repository = repository;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    public async Task<CidadeResponse> CreateAsync(CreateCidadeRequest request, CancellationToken cancellationToken = default)
    {
        var cidade = request.ToCidade();
        await _repository.AddAsync(cidade, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.cidade", cidade.Id.ToString(), cidade.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Cidade criada: {cidade.Id}");

        return cidade.ToCidadeResponse();
    }

    /// <summary>
    /// Obtém uma cidade por seu ID.
    /// </summary>
    public async Task<CidadeResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cidade = await _repository.GetByIdAsync(id, cancellationToken);
        return cidade?.ToCidadeResponse();
    }

    /// <summary>
    /// Lista todas as cidades com filtros opcionais.
    /// </summary>
    public async Task<IEnumerable<CidadeResponse>> ListAsync(string? estadoId = null, string? nome = null, bool? ativo = null, CancellationToken cancellationToken = default)
    {
        var cidades = await _repository.GetAllAsync(cancellationToken);

        var query = cidades.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(estadoId))
            query = query.Where(c => c.EstadoId == estadoId);

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (ativo.HasValue)
            query = query.Where(c => c.Ativo == ativo.Value);

        return query.Select(c => c.ToCidadeResponse());
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    public async Task<CidadeResponse?> UpdateAsync(Guid id, UpdateCidadeRequest request, CancellationToken cancellationToken = default)
    {
        var cidade = await _repository.GetByIdAsync(id, cancellationToken);
        if (cidade is null)
            return null;

        cidade.UpdateFromRequest(request);
        await _repository.UpdateAsync(cidade, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.cidade", cidade.Id.ToString(), cidade.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Cidade atualizada: {cidade.Id}");

        return cidade.ToCidadeResponse();
    }

    /// <summary>
    /// Remove uma cidade (soft-delete).
    /// </summary>
    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cidade = await _repository.GetByIdAsync(id, cancellationToken);
        if (cidade is null)
            return false;

        cidade.Ativo = false;
        await _repository.UpdateAsync(cidade, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.cidade", cidade.Id.ToString(), cidade.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"Cidade removida: {cidade.Id}");

        return true;
    }
}

