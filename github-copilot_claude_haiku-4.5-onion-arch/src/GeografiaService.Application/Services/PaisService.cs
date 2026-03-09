using GeografiaService.Application.DTOs;
using GeografiaService.Application.Extensions;
using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;

namespace GeografiaService.Application.Services;

/// <summary>
/// Serviço de negócios para gerenciar Países.
/// </summary>
public class PaisService
{
    private readonly IPaisRepository _repository;
    private readonly IEventProducer _eventProducer;
    private readonly ILogger<PaisService> _logger;

    /// <summary>
    /// Inicializa uma nova instância do serviço de País.
    /// </summary>
    public PaisService(IPaisRepository repository, IEventProducer eventProducer, ILogger<PaisService> logger)
    {
        _repository = repository;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    public async Task<PaisResponse> CreateAsync(CreatePaisRequest request, CancellationToken cancellationToken = default)
    {
        var pais = request.ToPais();
        await _repository.AddAsync(pais, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.pais", pais.Id, pais.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"País criado: {pais.Id}");

        return pais.ToPaisResponse();
    }

    /// <summary>
    /// Obtém um país por seu ID.
    /// </summary>
    public async Task<PaisResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var pais = await _repository.GetByIdAsync(id, cancellationToken);
        return pais?.ToPaisResponse();
    }

    /// <summary>
    /// Lista todos os países com filtros opcionais.
    /// </summary>
    public async Task<IEnumerable<PaisResponse>> ListAsync(string? nome = null, bool? ativo = null, CancellationToken cancellationToken = default)
    {
        var paises = await _repository.GetAllAsync(cancellationToken);

        var query = paises.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (ativo.HasValue)
            query = query.Where(p => p.Ativo == ativo.Value);

        return query.Select(p => p.ToPaisResponse());
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    public async Task<PaisResponse?> UpdateAsync(string id, UpdatePaisRequest request, CancellationToken cancellationToken = default)
    {
        var pais = await _repository.GetByIdAsync(id, cancellationToken);
        if (pais is null)
            return null;

        pais.UpdateFromRequest(request);
        await _repository.UpdateAsync(pais, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.pais", pais.Id, pais.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"País atualizado: {pais.Id}");

        return pais.ToPaisResponse();
    }

    /// <summary>
    /// Remove um país (soft-delete).
    /// </summary>
    public async Task<bool> RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var pais = await _repository.GetByIdAsync(id, cancellationToken);
        if (pais is null)
            return false;

        pais.Ativo = false;
        await _repository.UpdateAsync(pais, cancellationToken);

        await _eventProducer.ProduceEventAsync("geografia.pais", pais.Id, pais.ToKafkaJson(), cancellationToken);
        _logger.LogInformation($"País removido: {pais.Id}");

        return true;
    }
}

