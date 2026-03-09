namespace GeografiaService.Domain.Entities;

/// <summary>
/// Entidade que representa um país.
/// </summary>
public class Pais
{
    /// <summary>
    /// Identificador único do país no formato ISO 3166-1 alpha-2.
    /// Exemplo: "BR" para Brasil, "US" para Estados Unidos.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Nome comum do país.
    /// Exemplo: "Brasil", "Estados Unidos".
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-3 do país.
    /// Exemplo: "BRA" para Brasil, "USA" para Estados Unidos.
    /// </summary>
    public required string CodigoISO3 { get; set; }

    /// <summary>
    /// Código numérico da ONU (Organização das Nações Unidas).
    /// Exemplo: 076 para Brasil.
    /// </summary>
    public required int CodigoONU { get; set; }

    /// <summary>
    /// Código de discagem internacional (DDI) do país.
    /// Exemplo: "+55" para Brasil.
    /// </summary>
    public required string CodigoDDI { get; set; }

    /// <summary>
    /// Código da moeda do país conforme ISO 4217.
    /// Exemplo: "BRL" para Real Brasileiro.
    /// </summary>
    public required string CodigoMoeda { get; set; }

    /// <summary>
    /// Idioma principal/locale padrão do país.
    /// Exemplo: "pt-BR" para Brasil, "en-US" para Estados Unidos.
    /// </summary>
    public required string DefaultLocale { get; set; }

    /// <summary>
    /// Indicador se o país está ativo no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Coleção de estados/províncias que pertencem a este país.
    /// </summary>
    public ICollection<Estado> Estados { get; set; } = [];
}

