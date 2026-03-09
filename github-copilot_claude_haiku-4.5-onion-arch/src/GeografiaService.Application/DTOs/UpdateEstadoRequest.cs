using GeografiaService.Domain.Enums;

namespace GeografiaService.Application.DTOs;

/// <summary>
/// DTO para atualização de um estado.
/// </summary>
public record UpdateEstadoRequest
{
    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string? Nome { get; init; }

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public string? Sigla { get; init; }

    /// <summary>
    /// Tipo da subdivisão dentro do país.
    /// </summary>
    public EstadoTipo? Tipo { get; init; }
}

