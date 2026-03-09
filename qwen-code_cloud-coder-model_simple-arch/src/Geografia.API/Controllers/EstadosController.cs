using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Geografia.API.Infrastructure;
using Geografia.API.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.API.Controllers;

/// <summary>
/// Controller para gerenciamento de estados.
/// </summary>
[ApiController]
[Route("estados")]
public class EstadosController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly IKafkaProducerService _kafkaProducer;
    private const string Topic = "geografia.estado";

    /// <summary>
    /// Inicializa uma nova instância de <see cref="EstadosController"/>.
    /// </summary>
    public EstadosController(GeografiaDbContext context, IKafkaProducerService kafkaProducer)
    {
        _context = context;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EstadoResponseDto>> Create([FromBody] EstadoCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEstado();
        _context.Estados.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceCreateAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponseDto());
    }

    /// <summary>
    /// Obtém um estado por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoResponseDto>> GetById(string id, CancellationToken cancellationToken)
    {
        var entity = await _context.Estados.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        return entity.ToResponseDto();
    }

    /// <summary>
    /// Lista estados com filtragem opcional.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoResponseDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? sigla,
        [FromQuery] string? paisId,
        [FromQuery] string? tipo,
        [FromQuery] bool? ativo,
        CancellationToken cancellationToken)
    {
        var query = _context.Estados.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(e => e.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(sigla))
            query = query.Where(e => e.Sigla == sigla);

        if (!string.IsNullOrWhiteSpace(paisId))
            query = query.Where(e => e.PaisId == paisId);

        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(e => e.Tipo.ToString() == tipo.ToUpper());

        if (ativo.HasValue)
            query = query.Where(e => e.Ativo == ativo.Value);

        var result = await query.Select(e => e.ToResponseDto()).ToListAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<EstadoResponseDto>> Update(string id, [FromBody] EstadoUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Estados.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.UpdateFrom(dto);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceUpdateAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return Ok(entity.ToResponseDto());
    }

    /// <summary>
    /// Remove logicamente um estado (marca como inativo).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var entity = await _context.Estados.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.Ativo = false;
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceDeleteAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return NoContent();
    }
}
