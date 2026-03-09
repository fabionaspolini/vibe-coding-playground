namespace Geografia.Application.DTOs;

/// <summary>
/// DTO para criar um novo país.
/// </summary>
public class CriarPaisDto
{
    /// <summary>
    /// Identificador único no formato ISO 3166-1 alpha-2.
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
    /// DDI (Código de discagem) do país.
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em ISO 4217.
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal/padrão do país.
    /// </summary>
    public required string DefaultLocale { get; set; }
}

/// <summary>
/// DTO para atualizar um país existente.
/// </summary>
public class AtualizarPaisDto
{
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
    /// DDI (Código de discagem) do país.
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em ISO 4217.
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal/padrão do país.
    /// </summary>
    public required string DefaultLocale { get; set; }
}

/// <summary>
/// DTO para resposta de país.
/// </summary>
public class PaisDto
{
    /// <summary>
    /// Identificador único no formato ISO 3166-1 alpha-2.
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
    /// DDI (Código de discagem) do país.
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em ISO 4217.
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal/padrão do país.
    /// </summary>
    public required string DefaultLocale { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

