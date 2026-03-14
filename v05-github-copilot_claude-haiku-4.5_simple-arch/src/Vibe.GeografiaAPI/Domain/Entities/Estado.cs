namespace Vibe.GeografiaAPI.Domain.Entities;

/// <summary>
/// Enum que representa os tipos de subdivisões administrativas de um país.
/// Nem todos os países usam "Estado". O Canadá usa Províncias, a Colômbia usa Departamentos e a Argentina usa Províncias.
/// </summary>
public enum TipoEstado
{
    /// <summary>
    /// Estado (utilizado em países como Brasil, EUA, México, etc.)
    /// </summary>
    STATE,

    /// <summary>
    /// Província (utilizado no Canadá, Argentina, Irã, etc.)
    /// </summary>
    PROVINCE,

    /// <summary>
    /// Departamento (utilizado na Colômbia, Peru, França, etc.)
    /// </summary>
    DEPARTMENT,

    /// <summary>
    /// Distrito (utilizado em alguns países como Tailândia, Paquistão, etc.)
    /// </summary>
    DISTRICT
}

/// <summary>
/// Entidade que representa um estado, província, departamento ou distrito de um país.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único no formato do código ISO 3166-2.
    /// Exemplo: "BR-SP" (São Paulo, Brasil), "US-AK" (Alaska, EUA).
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-2 do país ao qual este estado pertence.
    /// Esta é a chave estrangeira para a entidade País.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Referência à entidade País (navegação).
    /// </summary>
    public Pais? Pais { get; set; }

    /// <summary>
    /// Nome do estado, província, departamento ou distrito.
    /// Exemplo: "São Paulo", "Santa Catarina", "Alaska".
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla ou código abreviado da subdivisão administrativa.
    /// Esta é a parte ISO 3166-2 sem o código do país.
    /// Exemplo: "SP" para "BR-SP", "AK" para "US-AK".
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão administrativa neste país.
    /// </summary>
    public required TipoEstado Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;
}

