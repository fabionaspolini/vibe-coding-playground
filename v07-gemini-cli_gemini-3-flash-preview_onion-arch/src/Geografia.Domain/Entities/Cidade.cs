namespace Geografia.Domain.Entities;

/// <summary>
/// Gerenciar cadastro de cidades.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Referência a Estado.
    /// </summary>
    public string EstadoId { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// CEP/Zip local.
    /// </summary>
    public string CodigoPostal { get; set; } = string.Empty;

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Navegação para o estado.
    /// </summary>
    public Estado? Estado { get; set; }
}
