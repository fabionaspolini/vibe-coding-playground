using Vibe.GeografiaAPI.Domain.Entities;

namespace Vibe.GeografiaAPI.Application.DTOs;

/// <summary>
/// DTO para requisições de criação de Estado.
/// </summary>
public class CreateEstadoDto
{
    /// <summary>
    /// Código ISO 3166-2 (identificador único).
    /// Exemplo: "BR-SP", "US-AK".
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-2 do país.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Nome do estado, província, departamento ou distrito.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla da subdivisão (parte ISO 3166-2 sem código do país).
    /// Exemplo: "SP", "AK".
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    public required TipoEstado Tipo { get; set; }
}

/// <summary>
/// DTO para requisições de atualização de Estado.
/// </summary>
public class UpdateEstadoDto
{
    /// <summary>
    /// Nome do estado, província, departamento ou distrito.
    /// </summary>
    public string? Nome { get; set; }

    /// <summary>
    /// Sigla da subdivisão.
    /// </summary>
    public string? Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    public TipoEstado? Tipo { get; set; }
}

/// <summary>
/// DTO para resposta de leitura de Estado.
/// </summary>
public class EstadoDto
{
    /// <summary>
    /// Código ISO 3166-2 (identificador único).
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Código ISO 3166-1 alpha-2 do país.
    /// </summary>
    public required string PaisId { get; set; }

    /// <summary>
    /// Nome do estado, província, departamento ou distrito.
    /// </summary>
    public required string Nome { get; set; }

    /// <summary>
    /// Sigla da subdivisão.
    /// </summary>
    public required string Sigla { get; set; }

    /// <summary>
    /// Tipo da subdivisão administrativa.
    /// </summary>
    public required TipoEstado Tipo { get; set; }

    /// <summary>
    /// Indicador se o registro ainda é válido.
    /// </summary>
    public bool Ativo { get; set; }
}

