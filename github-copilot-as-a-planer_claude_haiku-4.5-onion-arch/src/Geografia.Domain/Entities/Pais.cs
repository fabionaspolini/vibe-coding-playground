namespace Geografia.Domain.Entities;

/// <summary>
/// Entidade que representa um país no sistema.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único no formato ISO 3166-1 alpha-2.
    /// </summary>
    /// <example>BR, US</example>
    public required string Id { get; set; }

    /// <summary>
    /// Nome comum do país.
    /// </summary>
    /// <example>Brasil</example>
    public required string Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// </summary>
    /// <example>BRA</example>
    public required string CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU.
    /// </summary>
    /// <example>076</example>
    public required int CodigoONU { get; set; }

    /// <summary>
    /// DDI (Código de discagem) do país.
    /// </summary>
    /// <example>+55</example>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda em ISO 4217.
    /// </summary>
    /// <example>BRL</example>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal/padrão do país.
    /// </summary>
    /// <example>pt-BR, en-US</example>
    public required string DefaultLocale { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

