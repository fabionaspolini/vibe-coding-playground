namespace Geografia.Api.Dtos;

/// <summary>
/// Representa a requisição para criação/atualização de uma Cidade.
/// </summary>
public record CidadeRequest
{
    /// <summary>
    /// Identificador do Estado.
    /// </summary>
    public string EstadoId { get; init; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// CEP ou Zip code local.
    /// </summary>
    public string CodigoPostal { get; init; } = string.Empty;

    /// <summary>
    /// Latitude geográfica.
    /// </summary>
    public decimal Latitude { get; init; }

    /// <summary>
    /// Longitude geográfica.
    /// </summary>
    public decimal Longitude { get; init; }
}

/// <summary>
/// Representa a resposta de dados de uma Cidade.
/// </summary>
public record CidadeResponse
{
    /// <summary>
    /// Identificador único (UUID).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Identificador do Estado.
    /// </summary>
    public string EstadoId { get; init; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public string Nome { get; init; } = string.Empty;

    /// <summary>
    /// CEP ou Zip code local.
    /// </summary>
    public string CodigoPostal { get; init; } = string.Empty;

    /// <summary>
    /// Latitude geográfica.
    /// </summary>
    public decimal Latitude { get; init; }

    /// <summary>
    /// Longitude geográfica.
    /// </summary>
    public decimal Longitude { get; init; }

    /// <summary>
    /// Indica se a cidade está ativa.
    /// </summary>
    public bool Ativo { get; init; }
}
