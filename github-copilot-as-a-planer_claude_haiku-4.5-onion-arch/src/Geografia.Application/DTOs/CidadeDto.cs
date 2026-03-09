namespace Geografia.Application.DTOs;

/// <summary>
/// DTO para criar uma nova cidade.
/// </summary>
public class CriarCidadeDto
{
    /// <summary>
    /// Identificador do estado ao qual a cidade pertence.
    /// </summary>
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
    /// Coordenada de latitude.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public required decimal Longitude { get; set; }
}

/// <summary>
/// DTO para atualizar uma cidade existente.
/// </summary>
public class AtualizarCidadeDto
{
    /// <summary>
    /// Nome da cidade.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Código postal/CEP/Zip da cidade.
    /// </summary>
    public required string CodigoPostal { get; set; }

    /// <summary>
    /// Coordenada de latitude.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public required decimal Longitude { get; set; }
}

/// <summary>
/// DTO para resposta de cidade.
/// </summary>
public class CidadeDto
{
    /// <summary>
    /// Identificador único.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Identificador do estado ao qual a cidade pertence.
    /// </summary>
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
    /// Coordenada de latitude.
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// Coordenada de longitude.
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public required bool Ativo { get; set; }
}

