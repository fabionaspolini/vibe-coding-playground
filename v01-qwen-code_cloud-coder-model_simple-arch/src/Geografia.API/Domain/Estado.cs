namespace Geografia.API.Domain;

/// <summary>
/// Representa um estado ou subdivisão equivalente de um país.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único do estado, no formato ISO 3166-2.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Identificador do país ao qual o estado pertence.
    /// </summary>
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado (parte local do código ISO 3166-2).
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão (Estado, Província, Departamento, Distrito).
    /// </summary>
    public TipoEstado Tipo { get; set; }

    /// <summary>
    /// Indica se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// País ao qual o estado pertence.
    /// </summary>
    public virtual Pais? Pais { get; set; }

    /// <summary>
    /// Coleção de cidades pertencentes ao estado.
    /// </summary>
    public virtual ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}

/// <summary>
/// Tipos de subdivisão de estado.
/// </summary>
public enum TipoEstado
{
    /// <summary>
    /// Estado.
    /// </summary>
    STATE,

    /// <summary>
    /// Província.
    /// </summary>
    PROVINCE,

    /// <summary>
    /// Departamento.
    /// </summary>
    DEPARTMENT,

    /// <summary>
    /// Distrito.
    /// </summary>
    DISTRICT
}
