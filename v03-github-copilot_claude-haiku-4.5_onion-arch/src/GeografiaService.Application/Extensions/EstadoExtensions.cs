using GeografiaService.Application.DTOs;
using GeografiaService.Domain.Entities;
using System.Text.Json;

namespace GeografiaService.Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs de Estado.
/// </summary>
public static class EstadoExtensions
{
    /// <summary>
    /// Converte um CreateEstadoRequest para uma entidade Estado.
    /// </summary>
    public static Estado ToEstado(this CreateEstadoRequest request) => new()
    {
        Id = request.Id,
        PaisId = request.PaisId,
        Nome = request.Nome,
        Sigla = request.Sigla,
        Tipo = request.Tipo
    };

    /// <summary>
    /// Converte uma entidade Estado para EstadoResponse.
    /// </summary>
    public static EstadoResponse ToEstadoResponse(this Estado estado) => new()
    {
        Id = estado.Id,
        PaisId = estado.PaisId,
        Nome = estado.Nome,
        Sigla = estado.Sigla,
        Tipo = estado.Tipo,
        Ativo = estado.Ativo
    };

    /// <summary>
    /// Converte uma entidade Estado para JSON para envio ao Kafka.
    /// </summary>
    public static string ToKafkaJson(this Estado estado) => JsonSerializer.Serialize(new
    {
        estado.Id,
        estado.PaisId,
        estado.Nome,
        estado.Sigla,
        estado.Tipo,
        estado.Ativo,
        Timestamp = DateTime.UtcNow
    });

    /// <summary>
    /// Atualiza uma entidade Estado com os dados de UpdateEstadoRequest.
    /// </summary>
    public static void UpdateFromRequest(this Estado estado, UpdateEstadoRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Nome))
            estado.Nome = request.Nome;

        if (!string.IsNullOrWhiteSpace(request.Sigla))
            estado.Sigla = request.Sigla;

        if (request.Tipo.HasValue)
            estado.Tipo = request.Tipo.Value;
    }
}

