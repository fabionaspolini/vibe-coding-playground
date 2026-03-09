using GeografiaService.Domain.Enums;

namespace GeografiaService.Domain.Entities;

/// <summary>
/// Entidade que representa um estado, província, departamento ou distrito dentro de um país.
/// </summary>
public class Estado
{
    /// <summary>
    /// Identificador único do estado no formato ISO 3166-2.
    /// Exemplo: "BR-SP" para São Paulo, "BR-SC" para Santa Catarina, "US-AK" para Alaska.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Identificador do país ao qual este estado pertence.
    /// Referência para a entidade Pais.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Referência de navegação para o país.
    /// </summary>
    public Pais? Pais { get; set; }

    /// <summary>
    /// Nome completo do estado.
    /// Exemplo: "São Paulo", "Santa Catarina", "Paraná", "Alaska".
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla nacional do estado (parte do código ISO 3166-2 sem a prefixo do país).
    /// Exemplo: "SP" para São Paulo, "SC" para Santa Catarina.
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão dentro do país.
    /// Pode ser State, Province, Department, District ou Region.
    /// </summary>
    public required EstadoTipo Tipo { get; set; }

    /// <summary>
    /// Indicador se o estado está ativo no sistema.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Coleção de cidades que pertencem a este estado.
    /// </summary>
    public ICollection<Cidade> Cidades { get; set; } = [];
}

