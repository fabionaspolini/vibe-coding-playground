using GeografiaService.Application.DTOs;
using GeografiaService.Domain.Entities;
using System.Text.Json;

namespace GeografiaService.Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs de Cidade.
/// </summary>
public static class CidadeExtensions
{
    /// <summary>
    /// Converte um CreateCidadeRequest para uma entidade Cidade.
    /// </summary>
    public static Cidade ToCidade(this CreateCidadeRequest request) => new()
    {
        EstadoId = request.EstadoId,
        Nome = request.Nome,
        CodigoPostal = request.CodigoPostal,
        Latitude = request.Latitude,
        Longitude = request.Longitude
    };

    /// <summary>
    /// Converte uma entidade Cidade para CidadeResponse.
    /// </summary>
    public static CidadeResponse ToCidadeResponse(this Cidade cidade) => new()
    {
        Id = cidade.Id,
        EstadoId = cidade.EstadoId,
        Nome = cidade.Nome,
        CodigoPostal = cidade.CodigoPostal,
        Latitude = cidade.Latitude,
        Longitude = cidade.Longitude,
        Ativo = cidade.Ativo
    };

    /// <summary>
    /// Converte uma entidade Cidade para JSON para envio ao Kafka.
    /// </summary>
    public static string ToKafkaJson(this Cidade cidade) => JsonSerializer.Serialize(new
    {
        cidade.Id,
        cidade.EstadoId,
        cidade.Nome,
        cidade.CodigoPostal,
        cidade.Latitude,
        cidade.Longitude,
        cidade.Ativo,
        Timestamp = DateTime.UtcNow
    });

    /// <summary>
    /// Atualiza uma entidade Cidade com os dados de UpdateCidadeRequest.
    /// </summary>
    public static void UpdateFromRequest(this Cidade cidade, UpdateCidadeRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Nome))
            cidade.Nome = request.Nome;

        if (!string.IsNullOrWhiteSpace(request.CodigoPostal))
            cidade.CodigoPostal = request.CodigoPostal;

        if (request.Latitude.HasValue)
            cidade.Latitude = request.Latitude.Value;

        if (request.Longitude.HasValue)
            cidade.Longitude = request.Longitude.Value;
    }
}

