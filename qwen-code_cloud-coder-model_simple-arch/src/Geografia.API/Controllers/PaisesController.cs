using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Geografia.API.Infrastructure;
using Geografia.API.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Geografia.API.Controllers;

/// <summary>
/// Controller para gerenciamento de países.
/// </summary>
[ApiController]
[Route("paises")]
public class PaisesController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly IKafkaProducerService _kafkaProducer;
    private const string Topic = "geografia.pais";

    /// <summary>
    /// Inicializa uma nova instância de <see cref="PaisesController"/>.
    /// </summary>
    public PaisesController(GeografiaDbContext context, IKafkaProducerService kafkaProducer)
    {
        _context = context;
        _kafkaProducer = kafkaProducer;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PaisResponseDto>> Create([FromBody] PaisCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToPais();
        _context.Paises.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceCreateAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity.ToResponseDto());
    }

    /// <summary>
    /// Obtém um país por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PaisResponseDto>> GetById(string id, CancellationToken cancellationToken)
    {
        var entity = await _context.Paises.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        return entity.ToResponseDto();
    }

    /// <summary>
    /// Lista países com filtragem opcional.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaisResponseDto>>> List(
        [FromQuery] string? nome,
        [FromQuery] string? codigoISO3,
        [FromQuery] string? codigoMoeda,
        [FromQuery] bool? ativo,
        CancellationToken cancellationToken)
    {
        var query = _context.Paises.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(p => p.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(codigoISO3))
            query = query.Where(p => p.CodigoISO3 == codigoISO3);

        if (!string.IsNullOrWhiteSpace(codigoMoeda))
            query = query.Where(p => p.CodigoMoeda == codigoMoeda);

        if (ativo.HasValue)
            query = query.Where(p => p.Ativo == ativo.Value);

        var result = await query.Select(p => p.ToResponseDto()).ToListAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PaisResponseDto>> Update(string id, [FromBody] PaisUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await _context.Paises.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.UpdateFrom(dto);
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceUpdateAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return Ok(entity.ToResponseDto());
    }

    /// <summary>
    /// Remove logicamente um país (marca como inativo).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var entity = await _context.Paises.FindAsync([id], cancellationToken);
        if (entity is null)
            return NotFound();

        entity.Ativo = false;
        await _context.SaveChangesAsync(cancellationToken);

        await _kafkaProducer.ProduceDeleteAsync(Topic, entity.Id, entity.ToResponseDto(), cancellationToken);

        return NoContent();
    }
}
