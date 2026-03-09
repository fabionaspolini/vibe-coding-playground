namespace Geografia.Domain.Entities;

/// <summary>
/// Entidade que representa uma cidade em um estado.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único em formato UUID v7.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Identificador do estado ao qual a cidade pertence (ISO 3166-2).
    /// </summary>
    /// <example>BR-SP, US-AK</example>
    public required string EstadoId { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal/CEP/Zip da cidade.
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude para mapas e logística.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude para mapas e logística.
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

