using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Geografia.API.Infrastructure;
using Geografia.API.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.API.Controllers;

/// <summary>
/// Controller para gerenciamento de cidades.
/// </summary>
[ApiController]
[Route("cidades")]
public class CidadesController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly IKafkaProducerService _kafkaProducer;
    private const string Topic = "geografia.cidade";

    /// <summary>
    /// Inicializa uma nova instância de <see cref="CidadesController"/>.
    /// </summary>
    public CidadesController(GeografiaDbContext context, IKafkaProducerService kafkaProducer)
    {
        _context = context;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CidadeResponseDto>> Create([FromBody] CidadeCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToCidade();
        _context.Cidades.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceCreateAsync(Topic, entity.Id.ToString(), entity.ToResponseDto(), cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponseDto());
    }

    /// <summary>
    /// Obtém uma cidade por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CidadeResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Cidades.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        return entity.ToResponseDto();
    }

    /// <summary>
    /// Lista cidades com filtragem opcional.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CidadeResponseDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? estadoId,
        [FromQuery] string? codigoPostal,
        [FromQuery] bool? ativo,
        CancellationToken cancellationToken)
    {
        var query = _context.Cidades.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(c => c.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(estadoId))
            query = query.Where(c => c.EstadoId == estadoId);

        if (!string.IsNullOrWhiteSpace(codigoPostal))
            query = query.Where(c => c.CodigoPostal == codigoPostal);

        if (ativo.HasValue)
            query = query.Where(c => c.Ativo == ativo.Value);

        var result = await query.Select(c => c.ToResponseDto()).ToListAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CidadeResponseDto>> Update(Guid id, [FromBody] CidadeUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Cidades.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.UpdateFrom(dto);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceUpdateAsync(Topic, entity.Id.ToString(), entity.ToResponseDto(), cancellationToken);

        return Ok(entity.ToResponseDto());
    }

    /// <summary>
    /// Remove logicamente uma cidade (marca como inativo).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Cidades.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.Ativo = false;
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceDeleteAsync(Topic, entity.Id.ToString(), entity.ToResponseDto(), cancellationToken);

        return NoContent();
    }
}
