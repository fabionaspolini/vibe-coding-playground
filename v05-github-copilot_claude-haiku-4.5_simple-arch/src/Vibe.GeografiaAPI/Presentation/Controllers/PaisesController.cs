using Microsoft.AspNetCore.Mvc;
using Vibe.GeografiaAPI.Application.Data;
using Vibe.GeografiaAPI.Application.DTOs;
using Vibe.GeografiaAPI.Application.Events;
using Vibe.GeografiaAPI.Application.Extensions;
using Vibe.GeografiaAPI.Domain.Entities;

namespace Vibe.GeografiaAPI.Presentation.Controllers;

/// <summary>
/// Controller para gerenciamento de países.
/// Fornece endpoints REST para CRUD de entidades País.
/// </summary>
[ApiController]
[Route("paises")]
public class PaisesController : ControllerBase
{
    private readonly GeografiaDbContext _context;
    private readonly KafkaEventProducer _eventProducer;
    private readonly ILogger<PaisesController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do controller de países.
    /// </summary>
    public PaisesController(
        GeografiaDbContext context,
        KafkaEventProducer eventProducer,
        ILogger<PaisesController> logger)
    {
        _context = context;
        _eventProducer = eventProducer;
        _logger = logger;
    }

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    /// <param name="dto">Dados do país a ser criado.</param>
    /// <returns>O país criado com status 201.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PaisDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePaisDto dto)
    {
        try
        {
            var pais = dto.ToPaisEntity();
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.pais", pais.Id, new
            {
                action = "created",
                entityId = pais.Id,
                timestamp = DateTime.UtcNow
            });

            return CreatedAtAction(nameof(GetById), new { id = pais.Id }, pais.ToPaisDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar país");
            return BadRequest("Erro ao criar país");
        }
    }

    /// <summary>
    /// Obtém um país pelo seu ID.
    /// </summary>
    /// <param name="id">ID do país (código ISO 3166-1 alpha-2).</param>
    /// <returns>Os dados do país ou 404 se não encontrado.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var pais = await _context.Paises.FindAsync(id);
        if (pais == null)
            return NotFound();

        return Ok(pais.ToPaisDto());
    }

    /// <summary>
    /// Lista todos os países com filtros opcionais.
    /// </summary>
    /// <param name="nome">Filtro pelo nome (opcional).</param>
    /// <param name="ativo">Filtro por status ativo (opcional).</param>
    /// <returns>Lista de países que correspondem aos critérios.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<PaisDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        [FromQuery] string? nome,
        [FromQuery] bool? ativo)
    {
        var query = _context.Paises.AsQueryable();

        if (!string.IsNullOrEmpty(nome))
            query = query.Where(p => p.Nome.Contains(nome));

        if (ativo.HasValue)
            query = query.Where(p => p.Ativo == ativo.Value);

        var paises = await query.ToListAsync();
        return Ok(paises.Select(p => p.ToPaisDto()).ToList());
    }

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    /// <param name="id">ID do país a ser atualizado.</param>
    /// <param name="dto">Dados atualizados do país.</param>
    /// <returns>O país atualizado ou 404 se não encontrado.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PaisDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePaisDto dto)
    {
        try
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();

            pais.UpdateFromDto(dto);
            _context.Paises.Update(pais);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.pais", pais.Id, new
            {
                action = "updated",
                entityId = pais.Id,
                timestamp = DateTime.UtcNow
            });

            return Ok(pais.ToPaisDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar país {PaisId}", id);
            return BadRequest("Erro ao atualizar país");
        }
    }

    /// <summary>
    /// Remove um país (soft delete - marca como inativo).
    /// </summary>
    /// <param name="id">ID do país a ser removido.</param>
    /// <returns>Status 204 No Content se bem-sucedido, ou 404 se não encontrado.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string id)
    {
        try
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
                return NotFound();

            pais.Ativo = false;
            _context.Paises.Update(pais);
            await _context.SaveChangesAsync();

            _eventProducer.ProduceEvent("geografia.pais", pais.Id, new
            {
                action = "deleted",
                entityId = pais.Id,
                timestamp = DateTime.UtcNow
            });

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover país {PaisId}", id);
            return BadRequest("Erro ao remover país");
        }
    }
}

