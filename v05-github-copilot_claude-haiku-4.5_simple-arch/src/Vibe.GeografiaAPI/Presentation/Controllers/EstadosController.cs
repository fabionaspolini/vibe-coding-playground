using Microsoft.AspNetCore.Mvc;
using Vibe.GeografiaAPI.Application.Data;
using Vibe.GeografiaAPI.Application.DTOs;
using Vibe.GeografiaAPI.Application.Events;
using Vibe.GeografiaAPI.Application.Extensions;

namespace Vibe.GeografiaAPI.Presentation.Controllers;

/// <summary>
/// Controller para gerenciamento de estados, províncias, departamentos e distritos.
/// Fornece endpoints REST para CRUD de entidades Estado.
/// </summary>
[ApiController]
[Route("estados")]
public class EstadosController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly KafkaEventProducer _eventProducer;
    private readonly ILogger<EstadosController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do controller de estados.
    /// </summary>
    public EstadosController(
        GeografiaDbContext context,
        KafkaEventProducer eventProducer,
        ILogger<EstadosController> logger)
    {
        _context = context;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    /// <param name="dto">Dados do estado a ser criado.</param>
    /// <returns>O estado criado com status 201.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(EstadoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstadoDto dto)
    {
        try
        {
            var estado = dto.ToEstadoEntity();
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.estado", estado.Id, new
            {
                action = "created",
                entityId = estado.Id,
                timestamp = DateTime.UtcNow
            });

            return CreatedAtAction(nameof(GetById), new { id = estado.Id }, estado.ToEstadoDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar estado");
            return BadRequest("Erro ao criar estado");
        }
    }

    /// <summary>
    /// Obtém um estado pelo seu ID.
    /// </summary>
    /// <param name="id">ID do estado (código ISO 3166-2).</param>
    /// <returns>Os dados do estado ou 404 se não encontrado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EstadoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var estado = await _context.Estados.FindAsync(id);
        if (estado == null)
            return NotFound();

        return Ok(estado.ToEstadoDto());
    }

    /// <summary>
    /// Lista todos os estados com filtros opcionais.
    /// </summary>
    /// <param name="paisId">Filtro pelo ID do país (opcional).</param>
    /// <param name="nome">Filtro pelo nome (opcional).</param>
    /// <param name="ativo">Filtro por status ativo (opcional).</param>
    /// <returns>Lista de estados que correspondem aos critérios.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<EstadoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] string? paisId,
        [FromQuery] string? nome,
        [FromQuery] bool? ativo)
    {
        var query = _context.Estados.AsQueryable();

        if (!string.IsNullOrEmpty(paisId))
            query = query.Where(e => e.PaisId == paisId);

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(e => e.Nome.Contains(nome));

        if (ativo.HasValue)
            query = query.Where(e => e.Ativo == ativo.Value);

        var estados = await query.ToListAsync();
        return Ok(estados.Select(e => e.ToEstadoDto()).ToList());
    }

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    /// <param name="id">ID do estado a ser atualizado.</param>
    /// <param name="dto">Dados atualizados do estado.</param>
    /// <returns>O estado atualizado ou 404 se não encontrado.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(EstadoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEstadoDto dto)
    {
        try
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound();

            estado.UpdateFromDto(dto);
            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.estado", estado.Id, new
            {
                action = "updated",
                entityId = estado.Id,
                timestamp = DateTime.UtcNow
            });

            return Ok(estado.ToEstadoDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar estado {EstadoId}", id);
            return BadRequest("Erro ao atualizar estado");
        }
    }

    /// <summary>
    /// Remove um estado (soft delete - marca como inativo).
    /// </summary>
    /// <param name="id">ID do estado a ser removido.</param>
    /// <returns>Status 204 No Content se bem-sucedido, ou 404 se não encontrado.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string id)
    {
        try
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound();

            estado.Ativo = false;
            _context.Estados.Update(estado);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.estado", estado.Id, new
            {
                action = "deleted",
                entityId = estado.Id,
                timestamp = DateTime.UtcNow
            });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover estado {EstadoId}", id);
            return BadRequest("Erro ao remover estado");
        }
    }
}

