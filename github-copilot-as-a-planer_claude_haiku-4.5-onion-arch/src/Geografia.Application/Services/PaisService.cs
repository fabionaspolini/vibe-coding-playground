namespace Geografia.Application.Services;

using Geografia.Application.DTOs;
using Geografia.Application.Extensions;
using Geografia.Domain.Entities;
using Geografia.Infrastructure.Kafka;
using Geografia.Infrastructure.Repositories;

/// <summary>
/// Interface para serviço de Pais.
/// </summary>
public interface IPaisService
{
    /// <summary>
    /// Cria um novo país.
    /// </summary>
    /// <param name="dto">DTO para criar país.</param>
    Task<PaisDto> CreateAsync(CriarPaisDto dto);

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    /// <param name="id">ID do país.</param>
    /// <returns>DTO do país ou null.</returns>
    Task<PaisDto?> GetByIdAsync(string id);

    /// <summary>
    /// Lista todos os países com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    /// <returns>Lista de países.</returns>
    Task<List<PaisDto>> ListAsync(Dictionary<string, object>? filters = null);

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    /// <param name="id">ID do país.</param>
    /// <param name="dto">DTO para atualizar país.</param>
    Task<PaisDto> UpdateAsync(string id, AtualizarPaisDto dto);

    /// <summary>
    /// Remove (desativa) um país.
    /// </summary>
    /// <param name="id">ID do país.</param>
    Task RemoveAsync(string id);
}

/// <summary>
/// Implementação do serviço de Pais.
/// </summary>
public class PaisService : IPaisService
{
    private readonly IRepository<Pais> _repository;
    private readonly IKafkaProducer _kafkaProducer;

    /// <summary>
    /// Construtor do serviço de Pais.
    /// </summary>
    /// <param name="repository">Repositório de Pais.</param>
    /// <param name="kafkaProducer">Produtor Kafka.</param>
    public PaisService(IRepository<Pais> repository, IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    /// <param name="dto">DTO para criar país.</param>
    public async Task<PaisDto> CreateAsync(CriarPaisDto dto)
    {
        var pais = dto.ToPais();
        await _repository.AddAsync(pais);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.pais", pais.Id, new { action = "create", data = pais });

        return pais.ToDto();
    }

    /// <summary>
    /// Obtém um país pelo ID.
    /// </summary>
    /// <param name="id">ID do país.</param>
    public async Task<PaisDto?> GetByIdAsync(string id)
    {
        var pais = await _repository.GetByIdAsync(id);
        return pais?.ToDto();
    }

    /// <summary>
    /// Lista todos os países com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    public async Task<List<PaisDto>> ListAsync(Dictionary<string, object>? filters = null)
    {
        var paises = await _repository.ListAsync(filters);
        return paises.Select(p => p.ToDto()).ToList();
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    /// <param name="id">ID do país.</param>
    /// <param name="dto">DTO para atualizar país.</param>
    public async Task<PaisDto> UpdateAsync(string id, AtualizarPaisDto dto)
    {
        var pais = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"País com ID {id} não encontrado.");

        pais.UpdateFromDto(dto);
        await _repository.UpdateAsync(pais);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.pais", pais.Id, new { action = "update", data = pais });

        return pais.ToDto();
    }

    /// <summary>
    /// Remove (desativa) um país.
    /// </summary>
    /// <param name="id">ID do país.</param>
    public async Task RemoveAsync(string id)
    {
        var pais = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"País com ID {id} não encontrado.");

        pais.Ativo = false;
        await _repository.UpdateAsync(pais);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.pais", pais.Id, new { action = "delete", data = pais });
    }
}

