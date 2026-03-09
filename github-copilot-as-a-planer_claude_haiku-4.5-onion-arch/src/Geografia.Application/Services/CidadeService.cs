namespace Geografia.Application.Services;

using Geografia.Application.DTOs;
using Geografia.Application.Extensions;
using Geografia.Domain.Entities;
using Geografia.Infrastructure.Kafka;
using Geografia.Infrastructure.Repositories;

/// <summary>
/// Interface para serviço de Cidade.
/// </summary>
public interface ICidadeService
{
    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    /// <param name="dto">DTO para criar cidade.</param>
    Task<CidadeDto> CreateAsync(CriarCidadeDto dto);

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    /// <returns>DTO da cidade ou null.</returns>
    Task<CidadeDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Lista todas as cidades com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    /// <returns>Lista de cidades.</returns>
    Task<List<CidadeDto>> ListAsync(Dictionary<string, object>? filters = null);

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    /// <param name="dto">DTO para atualizar cidade.</param>
    Task<CidadeDto> UpdateAsync(Guid id, AtualizarCidadeDto dto);

    /// <summary>
    /// Remove (desativa) uma cidade.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    Task RemoveAsync(Guid id);
}

/// <summary>
/// Implementação do serviço de Cidade.
/// </summary>
public class CidadeService : ICidadeService
{
    private readonly IRepository<Cidade> _repository;
    private readonly IKafkaProducer _kafkaProducer;

    /// <summary>
    /// Construtor do serviço de Cidade.
    /// </summary>
    /// <param name="repository">Repositório de Cidade.</param>
    /// <param name="kafkaProducer">Produtor Kafka.</param>
    public CidadeService(IRepository<Cidade> repository, IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    /// <param name="dto">DTO para criar cidade.</param>
    public async Task<CidadeDto> CreateAsync(CriarCidadeDto dto)
    {
        var cidade = dto.ToCidade();
        await _repository.AddAsync(cidade);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.cidade", cidade.Id.ToString(), new { action = "create", data = cidade });

        return cidade.ToDto();
    }

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    public async Task<CidadeDto?> GetByIdAsync(Guid id)
    {
        var cidade = await _repository.GetByIdAsync(id);
        return cidade?.ToDto();
    }

    /// <summary>
    /// Lista todas as cidades com filtros opcionais.
    /// </summary>
    /// <param name="filters">Filtros a aplicar.</param>
    public async Task<List<CidadeDto>> ListAsync(Dictionary<string, object>? filters = null)
    {
        var cidades = await _repository.ListAsync(filters);
        return cidades.Select(c => c.ToDto()).ToList();
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    /// <param name="dto">DTO para atualizar cidade.</param>
    public async Task<CidadeDto> UpdateAsync(Guid id, AtualizarCidadeDto dto)
    {
        var cidade = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"Cidade com ID {id} não encontrada.");

        cidade.UpdateFromDto(dto);
        await _repository.UpdateAsync(cidade);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.cidade", cidade.Id.ToString(), new { action = "update", data = cidade });

        return cidade.ToDto();
    }

    /// <summary>
    /// Remove (desativa) uma cidade.
    /// </summary>
    /// <param name="id">ID da cidade.</param>
    public async Task RemoveAsync(Guid id)
    {
        var cidade = await _repository.GetByIdAsync(id) 
            ?? throw new KeyNotFoundException($"Cidade com ID {id} não encontrada.");

        cidade.Ativo = false;
        await _repository.UpdateAsync(cidade);
        await _repository.SaveChangesAsync();

        _kafkaProducer.Produce("geografia.cidade", cidade.Id.ToString(), new { action = "delete", data = cidade });
    }
}

