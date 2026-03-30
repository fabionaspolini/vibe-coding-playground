namespace Geografia.Application.Dtos;

/// <summary>
/// Data Transfer Object para Pais.
/// </summary>
public record PaisDto
{
    /// <summary>
    /// Identificador único (ISO 3166-1 alpha-2).
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; init; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; init; }

    /// <summary>
    /// DDI (Código de discagem).
    /// </summary>
    public string CodigoDDI { get; init; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; init; } = string.Empty;

    /// <summary>
    /// Idioma principal.
    /// </summary>
    public string DefaultLocale { get; init; } = string.Empty;

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; init; }
}

/// <summary>
/// Request para criação/atualização de Pais.
/// </summary>
public record PaisRequest
{
    /// <summary>
    /// Identificador único (ISO 3166-1 alpha-2).
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; init; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; init; }

    /// <summary>
    /// DDI (Código de discagem).
    /// </summary>
    public string CodigoDDI { get; init; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; init; } = string.Empty;

    /// <summary>
    /// Idioma principal.
    /// </summary>
    public string DefaultLocale { get; init; } = string.Empty;
}
