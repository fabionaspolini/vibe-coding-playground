using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeografiaService.API.Controllers;

/// <summary>
/// Controller para gerenciar operações relacionadas a Cidades.
/// </summary>
[ApiController]
[Route("cidades")]
public class CidadeController : ControllerBase
{
    private readonly CidadeService _service;

    /// <summary>
    /// Inicializa uma nova instância do controller de Cidade.
    /// </summary>
    public CidadeController(CidadeService service) => _service = service;

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    /// <param name="request">Dados da cidade a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CidadeResponse>> Create([FromBody] CreateCidadeRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Obtém uma cidade por seu ID.
    /// </summary>
    /// <param name="id">Identificador da cidade (UUID).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CidadeResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Lista todas as cidades com filtros opcionais.
    /// </summary>
    /// <param name="estadoId">Filtro opcional por ID do estado.</param>
    /// <param name="nome">Filtro opcional por nome da cidade.</param>
    /// <param name="ativo">Filtro opcional por status ativo/inativo.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CidadeResponse>>> List(
        [FromQuery] string? estadoId = null,
        [FromQuery] string? nome = null,
        [FromQuery] bool? ativo = null,
        CancellationToken cancellationToken = default) =>
        Ok(await _service.ListAsync(estadoId, nome, ativo, cancellationToken));

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    /// <param name="id">Identificador da cidade (UUID).</param>
    /// <param name="request">Dados a serem atualizados.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CidadeResponse>> Update(Guid id, [FromBody] UpdateCidadeRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Remove uma cidade (soft-delete).
    /// </summary>
    /// <param name="id">Identificador da cidade (UUID).</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.RemoveAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}

