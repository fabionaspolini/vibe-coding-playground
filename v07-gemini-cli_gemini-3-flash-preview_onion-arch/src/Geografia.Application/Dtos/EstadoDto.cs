using Geografia.Domain.Enums;

namespace Geografia.Application.Dtos;

/// <summary>
/// Data Transfer Object para Estado.
/// </summary>
public record EstadoDto
{
    /// <summary>
    /// Identificador único (ISO 3166-2).
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Referência a Pais.
    /// </summary>
    public string PaisId { get; init; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public string Sigla { get; init; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public TipoEstado Tipo { get; init; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; init; }
}

/// <summary>
/// Request para criação/atualização de Estado.
/// </summary>
public record EstadoRequest
{
    /// <summary>
    /// Identificador único (ISO 3166-2).
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Referência a Pais.
    /// </summary>
    public string PaisId { get; init; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado.
    /// </summary>
    public string Sigla { get; init; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public TipoEstado Tipo { get; init; }
}
