namespace Geografia.API.DTOs;

/// <summary>
/// DTO para criação de um país.
/// </summary>
public class PaisCreateDto
{
    /// <summary>
    /// Identificador único do país (ISO 3166-1 alpha-2).
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código DDI.
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão.
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;
}

/// <summary>
/// DTO para atualização de um país.
/// </summary>
public class PaisUpdateDto
{
    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código DDI.
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda (ISO 4217).
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão.
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;
}

/// <summary>
/// DTO para resposta de um país.
/// </summary>
public class PaisResponseDto
{
    /// <summary>
    /// Identificador único do país.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    public string CodigoISO3 { get; set; } = string.Empty;

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    public int CodigoONU { get; set; }

    /// <summary>
    /// Código DDI.
    /// </summary>
    public string CodigoDDI { get; set; } = string.Empty;

    /// <summary>
    /// Código da moeda.
    /// </summary>
    public string CodigoMoeda { get; set; } = string.Empty;

    /// <summary>
    /// Locale padrão.
    /// </summary>
    public string DefaultLocale { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; }
}
