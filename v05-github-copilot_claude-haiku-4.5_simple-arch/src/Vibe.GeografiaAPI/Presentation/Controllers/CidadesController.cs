using Microsoft.AspNetCore.Mvc;
using Vibe.GeografiaAPI.Application.Data;
using Vibe.GeografiaAPI.Application.DTOs;
using Vibe.GeografiaAPI.Application.Events;
using Vibe.GeografiaAPI.Application.Extensions;

namespace Vibe.GeografiaAPI.Presentation.Controllers;

/// <summary>
/// Controller para gerenciamento de cidades.
/// Fornece endpoints REST para CRUD de entidades Cidade.
/// </summary>
[ApiController]
[Route("cidades")]
public class CidadesController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly KafkaEventProducer _eventProducer;
    private readonly ILogger<CidadesController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do controller de cidades.
    /// </summary>
    public CidadesController(
        GeografiaDbContext context,
        KafkaEventProducer eventProducer,
        ILogger<CidadesController> logger)
    {
        _context = context;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    /// <param name="dto">Dados da cidade a ser criada.</param>
    /// <returns>A cidade criada com status 201.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CidadeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCidadeDto dto)
    {
        try
        {
            var cidade = dto.ToCidadeEntity();
            _context.Cidades.Add(cidade);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.cidade", cidade.Id.ToString(), new
            {
                action = "created",
                entityId = cidade.Id,
                timestamp = DateTime.UtcNow
            });

            return CreatedAtAction(nameof(GetById), new { id = cidade.Id }, cidade.ToCidadeDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar cidade");
            return BadRequest("Erro ao criar cidade");
        }
    }

    /// <summary>
    /// Obtém uma cidade pelo seu ID.
    /// </summary>
    /// <param name="id">ID da cidade (UUID v7).</param>
    /// <returns>Os dados da cidade ou 404 se não encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CidadeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var cidade = await _context.Cidades.FindAsync(id);
        if (cidade == null)
            return NotFound();

        return Ok(cidade.ToCidadeDto());
    }

    /// <summary>
    /// Lista todas as cidades com filtros opcionais.
    /// </summary>
    /// <param name="estadoId">Filtro pelo ID do estado (opcional).</param>
    /// <param name="nome">Filtro pelo nome (opcional).</param>
    /// <param name="codigoPostal">Filtro pelo código postal (opcional).</param>
    /// <param name="ativo">Filtro por status ativo (opcional).</param>
    /// <returns>Lista de cidades que correspondem aos critérios.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CidadeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] string? estadoId,
        [FromQuery] string? nome,
        [FromQuery] string? codigoPostal,
        [FromQuery] bool? ativo)
    {
        var query = _context.Cidades.AsQueryable();

        if (!string.IsNullOrEmpty(estadoId))
            query = query.Where(c => c.EstadoId == estadoId);

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(c => c.Nome.Contains(nome));

        if (!string.IsNullOrEmpty(codigoPostal))
            query = query.Where(c => c.CodigoPostal == codigoPostal);

        if (ativo.HasValue)
            query = query.Where(c => c.Ativo == ativo.Value);

        var cidades = await query.ToListAsync();
        return Ok(cidades.Select(c => c.ToCidadeDto()).ToList());
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    /// <param name="id">ID da cidade a ser atualizada.</param>
    /// <param name="dto">Dados atualizados da cidade.</param>
    /// <returns>A cidade atualizada ou 404 se não encontrada.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CidadeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCidadeDto dto)
    {
        try
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
                return NotFound();

            cidade.UpdateFromDto(dto);
            _context.Cidades.Update(cidade);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.cidade", cidade.Id.ToString(), new
            {
                action = "updated",
                entityId = cidade.Id,
                timestamp = DateTime.UtcNow
            });

            return Ok(cidade.ToCidadeDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar cidade {CidadeId}", id);
            return BadRequest("Erro ao atualizar cidade");
        }
    }

    /// <summary>
    /// Remove uma cidade (soft delete - marca como inativa).
    /// </summary>
    /// <param name="id">ID da cidade a ser removida.</param>
    /// <returns>Status 204 No Content se bem-sucedido, ou 404 se não encontrada.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(Guid id)
    {
        try
        {
            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
                return NotFound();

            cidade.Ativo = false;
            _context.Cidades.Update(cidade);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.cidade", cidade.Id.ToString(), new
            {
                action = "deleted",
                entityId = cidade.Id,
                timestamp = DateTime.UtcNow
            });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover cidade {CidadeId}", id);
            return BadRequest("Erro ao remover cidade");
        }
    }
}

