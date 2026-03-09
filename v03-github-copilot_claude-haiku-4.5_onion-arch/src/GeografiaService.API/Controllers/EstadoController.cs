using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeografiaService.API.Controllers;

/// <summary>
/// Controller para gerenciar operações relacionadas a Estados.
/// </summary>
[ApiController]
[Route("estados")]
public class EstadoController : ControllerBase
{
    private readonly EstadoService _service;

    /// <summary>
    /// Inicializa uma nova instância do controller de Estado.
    /// </summary>
    public EstadoController(EstadoService service) => _service = service;

    /// <summary>
    /// Cria um novo estado.
    /// </summary>
    /// <param name="request">Dados do estado a ser criado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoResponse>> Create([FromBody] CreateEstadoRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtém um estado por seu ID.
    /// </summary>
    /// <param name="id">Identificador do estado (ISO 3166-2).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstadoResponse>> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Lista todos os estados com filtros opcionais.
    /// </summary>
    /// <param name="paisId">Filtro opcional por ID do país.</param>
    /// <param name="nome">Filtro opcional por nome do estado.</param>
    /// <param name="ativo">Filtro opcional por status ativo/inativo.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EstadoResponse>>> List(
        [FromQuery] string? paisId = null,
        [FromQuery] string? nome = null,
        [FromQuery] bool? ativo = null,
        CancellationToken cancellationToken = default) =>
        Ok(await _service.ListAsync(paisId, nome, ativo, cancellationToken));

    /// <summary>
    /// Atualiza um estado existente.
    /// </summary>
    /// <param name="id">Identificador do estado (ISO 3166-2).</param>
    /// <param name="request">Dados a serem atualizados.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoResponse>> Update(string id, [FromBody] UpdateEstadoRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Remove um estado (soft-delete).
    /// </summary>
    /// <param name="id">Identificador do estado (ISO 3166-2).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        var result = await _service.RemoveAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}

