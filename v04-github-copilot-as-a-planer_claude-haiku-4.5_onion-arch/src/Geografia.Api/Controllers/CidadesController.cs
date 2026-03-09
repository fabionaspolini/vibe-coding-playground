namespace Geografia.Api.Controllers;

using Geografia.Application.DTOs;
using Geografia.Application.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller para gerenciar cidades.
/// </summary>
[ApiController]
[Route("cidades")]
// [Authorize] // TODO: Ativar quando implementar autenticação JWT
public class CidadesController : ControllerBase
{
    private readonly ICidadeService _service;

    /// <summary>
    /// Construtor da controller de cidades.
    /// </summary>
    /// <param name="service">Serviço de cidades.</param>
    public CidadesController(ICidadeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria uma nova cidade.
    /// </summary>
    /// <param name="dto">Dados da cidade a ser criada.</param>
    /// <returns>Código 201 e os dados da cidade criada.</returns>
    [HttpPost]
    public async Task<ActionResult<CidadeDto>> Create([FromBody] CriarCidadeDto dto)
    {
        var cidade = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = cidade.Id }, cidade);
    }

    /// <summary>
    /// Obtém uma cidade pelo ID.
    /// </summary>
    /// <param name="id">ID da cidade (UUID).</param>
    /// <returns>Dados da cidade ou 404 se não encontrada.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CidadeDto>> GetById(Guid id)
    {
        var cidade = await _service.GetByIdAsync(id);
        if (cidade == null)
            return NotFound();

        return Ok(cidade);
    }

    /// <summary>
    /// Lista cidades com filtros opcionais.
    /// </summary>
    /// <param name="filters">Parâmetros de query para filtrar por atributos.</param>
    /// <returns>Lista de cidades.</returns>
    [HttpGet]
    public async Task<ActionResult<List<CidadeDto>>> List([FromQuery] Dictionary<string, string> filters)
    {
        var filterDict = filters?.ToDictionary(x => x.Key, x => (object)x.Value);
        var cidades = await _service.ListAsync(filterDict);
        return Ok(cidades);
    }

    /// <summary>
    /// Atualiza uma cidade existente.
    /// </summary>
    /// <param name="id">ID da cidade a ser atualizada.</param>
    /// <param name="dto">Dados atualizados da cidade.</param>
    /// <returns>Dados da cidade atualizada.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CidadeDto>> Update(Guid id, [FromBody] AtualizarCidadeDto dto)
    {
        var cidade = await _service.UpdateAsync(id, dto);
        return Ok(cidade);
    }

    /// <summary>
    /// Remove (desativa) uma cidade.
    /// </summary>
    /// <param name="id">ID da cidade a ser removida.</param>
    /// <returns>Código 204 No Content.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _service.RemoveAsync(id);
        return NoContent();
    }
}

