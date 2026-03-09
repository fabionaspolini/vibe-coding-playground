namespace Geografia.Domain.Entities;

/// <summary>
/// Enum que define os tipos de subdivisões de estados/províncias.
/// </summary>
public enum TipoEstado
{
    /// <summary>
    /// Estado padrão.
    /// </summary>
    State,

    /// <summary>
    /// Província.
    /// </summary>
    Province,

    /// <summary>
    /// Departamento.
    /// </summary>
    Department,

    /// <summary>
    /// Distrito.
    /// </summary>
    District
}

/// <summary>
/// Entidade que representa um estado/província/departamento em um país.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único no formato ISO 3166-2.
    /// </summary>
    /// <example>BR-SP, BR-SC, US-AK</example>
    public required string Id { get; set; }

    /// <summary>
    /// Identificador do país ao qual o estado pertence (ISO 3166-1 alpha-2).
    /// </summary>
    /// <example>BR, US</example>
    public required string PaisId { get; set; }

    /// <summary>
    /// Nome do estado/província/departamento.
    /// </summary>
    /// <example>São Paulo, Santa Catarina, Alaska</example>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla do estado (código ISO 3166-2 sem a parte inicial do país).
    /// </summary>
    /// <example>SP, SC, AK</example>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo de subdivisão no país.
    /// </summary>
    public required TipoEstado Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

