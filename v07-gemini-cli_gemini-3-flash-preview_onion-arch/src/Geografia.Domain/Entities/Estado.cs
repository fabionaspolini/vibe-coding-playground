using Geografia.Domain.Enums;

namespace Geografia.Domain.Entities;

/// <summary>
/// Gerenciar cadastro de estados.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único, sendo no formato do código ISO 3166-2.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Referência a entidade Pais.
    /// </summary>
    public string PaisId { get; set; } = string.Empty;

    /// <summary>
    /// Nome do estado.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Sigla nacional do estado (Código ISO 3166-2 sem a parte inicial do país).
    /// </summary>
    public string Sigla { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da subdivisão no país.
    /// </summary>
    public TipoEstado Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Navegação para o país.
    /// </summary>
    public Pais? Pais { get; set; }
}
