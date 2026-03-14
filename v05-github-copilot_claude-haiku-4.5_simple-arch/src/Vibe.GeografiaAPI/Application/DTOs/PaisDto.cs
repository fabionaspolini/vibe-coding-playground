namespace Vibe.GeografiaAPI.Application.DTOs;

/// <summary>
/// DTO para requisições de criação de País.
/// </summary>
public class CreatePaisDto
{
    /// <summary>
    /// Código ISO 3166-1 alpha-2 (identificador único).
    /// Exemplo: "BR", "US".
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// Exemplo: "BRA", "USA".
    /// </summary>
    public required string CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public required int CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// Exemplo: "+55", "+1".
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em padrão ISO 4217.
    /// Exemplo: "BRL", "USD".
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal em formato locale.
    /// Exemplo: "pt-BR", "en-US".
    /// </summary>
    public required string DefaultLocale { get; set; }
}

/// <summary>
/// DTO para requisições de atualização de País.
/// </summary>
public class UpdatePaisDto
{
    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string? Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string? CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int? CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    public string? CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em padrão ISO 4217.
    /// </summary>
    public string? CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal em formato locale.
    /// </summary>
    public string? DefaultLocale { get; set; }
}

/// <summary>
/// DTO para resposta de leitura de País.
/// </summary>
public class PaisDto
{
    /// <summary>
    /// Código ISO 3166-1 alpha-2 (identificador único).
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public required string CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public required int CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em padrão ISO 4217.
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal em formato locale.
    /// </summary>
    public required string DefaultLocale { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; }
}

