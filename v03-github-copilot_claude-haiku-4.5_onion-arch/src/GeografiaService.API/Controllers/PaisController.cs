using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeografiaService.API.Controllers;

/// <summary>
/// Controller para gerenciar operações relacionadas a Países.
/// </summary>
[ApiController]
[Route("paises")]
public class PaisController : ControllerBase
{
    private readonly PaisService _service;

    /// <summary>
    /// Inicializa uma nova instância do controller de País.
    /// </summary>
    public PaisController(PaisService service) => _service = service;

    /// <summary>
    /// Cria um novo país.
    /// </summary>
    /// <param name="request">Dados do país a ser criado.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisResponse>> Create([FromBody] CreatePaisRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtém um país por seu ID.
    /// </summary>
    /// <param name="id">Identificador do país (ISO 3166-1 alpha-2).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaisResponse>> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Lista todos os países com filtros opcionais.
    /// </summary>
    /// <param name="nome">Filtro opcional por nome do país.</param>
    /// <param name="ativo">Filtro opcional por status ativo/inativo.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaisResponse>>> List(
        [FromQuery] string? nome = null,
        [FromQuery] bool? ativo = null,
        CancellationToken cancellationToken = default) =>
        Ok(await _service.ListAsync(nome, ativo, cancellationToken));

    /// <summary>
    /// Atualiza um país existente.
    /// </summary>
    /// <param name="id">Identificador do país (ISO 3166-1 alpha-2).</param>
    /// <param name="request">Dados a serem atualizados.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaisResponse>> Update(string id, [FromBody] UpdatePaisRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Remove um país (soft-delete).
    /// </summary>
    /// <param name="id">Identificador do país (ISO 3166-1 alpha-2).</param>
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

