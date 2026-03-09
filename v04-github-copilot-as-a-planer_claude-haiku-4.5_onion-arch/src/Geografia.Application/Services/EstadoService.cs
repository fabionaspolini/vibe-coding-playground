namespace Geografia.Application.Services;

using Geografia.Application.DTOs;
using Geografia.Application.Extensions;
using Geografia.Domain.Entities;
using Geografia.Infrastructure.Kafka;
using Geografia.Infrastructure.Repositories;

/// <summary>
/// Interface para serviço de Estado.
/// </summary>
public interface IEstadoService
{
    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    /// <param name="dto">DTO para criar estado.</param>
    Task<EstadoDto> CreateAsync(CriarEstadoDto dto);

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    /// <returns>DTO do estado ou null.</returns>
    Task<EstadoDto?> GetByIdAsync(string id);

    /// <summary>
    /// Lista todos os estados com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    /// <returns>Lista de estados.</returns>
    Task<List<EstadoDto>> ListAsync(Dictionary<string, object>? filters = null);

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    /// <param name="dto">DTO para atualizar estado.</param>
    Task<EstadoDto> UpdateAsync(string id, AtualizarEstadoDto dto);

    /// <summary>
    /// Remove (desativa) um estado.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    Task RemoveAsync(string id);
}

/// <summary>
/// Implementação do serviço de Estado.
/// </summary>
public class EstadoService : IEstadoService
{
    private readonly IRepository<Estado> _repository;
    private readonly IKafkaProducer _kafkaProducer;

    /// <summary>
    /// Construtor do serviço de Estado.
    /// </summary>
    /// <param name="repository">Repositório de Estado.</param>
    /// <param name="kafkaProducer">Produtor Kafka.</param>
    public EstadoService(IRepository<Estado> repository, IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    /// <param name="dto">DTO para criar estado.</param>
    public async Task<EstadoDto> CreateAsync(CriarEstadoDto dto)
    {
        var estado = dto.ToEstado();
        await _repository.AddAsync(estado);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.estado", estado.Id, new { action = "create", data = estado });

        return estado.ToDto();
    }

    /// <summary>
    /// Obtém um estado pelo ID.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    public async Task<EstadoDto?> GetByIdAsync(string id)
    {
        var estado = await _repository.GetByIdAsync(id);
        return estado?.ToDto();
    }

    /// <summary>
    /// Lista todos os estados com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    public async Task<List<EstadoDto>> ListAsync(Dictionary<string, object>? filters = null)
    {
        var estados = await _repository.ListAsync(filters);
        return estados.Select(e => e.ToDto()).ToList();
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    /// <param name="dto">DTO para atualizar estado.</param>
    public async Task<EstadoDto> UpdateAsync(string id, AtualizarEstadoDto dto)
    {
        var estado = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"Estado com ID {id} não encontrado.");

        estado.UpdateFromDto(dto);
        await _repository.UpdateAsync(estado);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.estado", estado.Id, new { action = "update", data = estado });

        return estado.ToDto();
    }

    /// <summary>
    /// Remove (desativa) um estado.
    /// </summary>
    /// <param name="id">ID do estado.</param>
    public async Task RemoveAsync(string id)
    {
        var estado = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"Estado com ID {id} não encontrado.");

        estado.Ativo = false;
        await _repository.UpdateAsync(estado);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.estado", estado.Id, new { action = "delete", data = estado });
    }
}

