namespace Vibe.GeografiaAPI.Domain.Entities;

/// <summary>
/// Entidade que representa um país no sistema de gerenciamento de dados geográficos.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único no formato do código ISO 3166-1 alpha-2.
    /// Exemplo: "BR", "US", "FR".
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Nome comum do país.
    /// Exemplo: "Brasil", "Estados Unidos", "França".
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3.
    /// Exemplo: "BRA", "USA", "FRA".
    /// </summary>
    public required string CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU (Organização das Nações Unidas).
    /// Exemplo: 076 para Brasil.
    /// </summary>
    public required int CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI).
    /// Exemplo: "+55" para Brasil, "+1" para EUA.
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda no padrão ISO 4217.
    /// Exemplo: "BRL", "USD", "EUR".
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal do país no formato locale.
    /// Exemplo: "pt-BR", "en-US", "fr-FR".
    /// </summary>
    public required string DefaultLocale { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;
}

