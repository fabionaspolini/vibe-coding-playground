namespace Vibe.GeografiaAPI.Domain.Entities;

/// <summary>
/// Entidade que representa uma cidade no sistema de gerenciamento de dados geográficos.
/// </summary>
public class Cidade
{
    /// <summary>
    /// Identificador único no formato UUID v7.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Código ISO 3166-2 do estado ao qual esta cidade pertence.
    /// Esta é a chave estrangeira para a entidade Estado.
    /// </summary>
    public required string EstadoId { get; set; }

    /// <summary>
    /// Referência à entidade Estado (navegação).
    /// </summary>
    public Estado? Estado { get; set; }

    /// <summary>
    /// Nome da cidade.
    /// Exemplo: "São Paulo", "Rio de Janeiro", "New York".
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal da cidade (CEP no Brasil, Zip nos EUA, etc.).
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude para mapas e logística.
    /// Valores válidos: -90.0 a 90.0
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude para mapas e logística.
    /// Valores válidos: -180.0 a 180.0
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; } = true;
}

