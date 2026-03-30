namespace Geografia.Application.Dtos;

/// <summary>
/// Data Transfer Object para Cidade.
/// </summary>
public record CidadeDto
{
    /// <summary>
    /// Identificador único.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Referência a Estado.
    /// </summary>
    public string EstadoId { get; init; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// CEP/Zip local.
    /// </summary>
    public string CodigoPostal { get; init; } = string.Empty;

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Latitude { get; init; }

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Longitude { get; init; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; init; }
}

/// <summary>
/// Request para criação/atualização de Cidade.
/// </summary>
public record CidadeRequest
{
    /// <summary>
    /// Referência a Estado.
    /// </summary>
    public string EstadoId { get; init; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// CEP/Zip local.
    /// </summary>
    public string CodigoPostal { get; init; } = string.Empty;

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Latitude { get; init; }

    /// <summary>
    /// Coordenada para mapas e logística.
    /// </summary>
    public decimal Longitude { get; init; }
}
