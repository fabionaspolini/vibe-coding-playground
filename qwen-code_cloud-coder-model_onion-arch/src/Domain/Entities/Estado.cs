using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Representa uma subdivisão administrativa de um país (estado, província, departamento, etc.).
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único no formato ISO 3166-2 (ex: BR-SP, US-CA).
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
    /// Sigla nacional do estado (sem o prefixo do país).
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão (estado, província, departamento, distrito).
    /// </summary>
    public SubdivisionType Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro está ativo.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// País ao qual o estado pertence.
    /// </summary>
    public virtual Pais? Pais { get; set; }

    /// <summary>
    /// Coleção de cidades do estado.
    /// </summary>
    public virtual ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}
