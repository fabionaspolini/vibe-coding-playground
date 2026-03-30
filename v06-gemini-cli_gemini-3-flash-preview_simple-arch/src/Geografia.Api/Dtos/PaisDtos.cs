namespace Geografia.Api.Dtos;

/// <summary>
/// Representa a requisição para criação/atualização de um País.
/// </summary>
public record PaisRequest
{
    /// <summary>
    /// Identificador único, no formato ISO 3166-1 alpha-2 (ex: "BR").
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3 (ex: "BRA").
    /// </summary>
    public string CodigoISO3 { get; init; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; init; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    public string CodigoDDI { get; init; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; init; } = string.Empty;

    /// <summary>
    /// Idioma principal do país.
    /// </summary>
    public string DefaultLocale { get; init; } = string.Empty;
}

/// <summary>
/// Representa a resposta de dados de um País.
/// </summary>
public record PaisResponse
{
    /// <summary>
    /// Identificador único do país.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Nome do país.
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
    /// Código de discagem internacional (DDI).
    /// </summary>
    public string CodigoDDI { get; init; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; init; } = string.Empty;

    /// <summary>
    /// Idioma principal do país.
    /// </summary>
    public string DefaultLocale { get; init; } = string.Empty;

    /// <summary>
    /// Indica se o país está ativo.
    /// </summary>
    public bool Ativo { get; init; }
}
