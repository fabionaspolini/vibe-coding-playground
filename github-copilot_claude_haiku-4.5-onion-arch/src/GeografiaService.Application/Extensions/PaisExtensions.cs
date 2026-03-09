using GeografiaService.Application.DTOs;
using GeografiaService.Domain.Entities;
using System.Text.Json;

namespace GeografiaService.Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs de País.
/// </summary>
public static class PaisExtensions
{
    /// <summary>
    /// Converte um CreatePaisRequest para uma entidade Pais.
    /// </summary>
    public static Pais ToPais(this CreatePaisRequest request) => new()
    {
        Id = request.Id,
        Nome = request.Nome,
        CodigoISO3 = request.CodigoISO3,
        CodigoONU = request.CodigoONU,
        CodigoDDI = request.CodigoDDI,
        CodigoMoeda = request.CodigoMoeda,
        DefaultLocale = request.DefaultLocale
    };

    /// <summary>
    /// Converte uma entidade Pais para PaisResponse.
    /// </summary>
    public static PaisResponse ToPaisResponse(this Pais pais) => new()
    {
        Id = pais.Id,
        Nome = pais.Nome,
        CodigoISO3 = pais.CodigoISO3,
        CodigoONU = pais.CodigoONU,
        CodigoDDI = pais.CodigoDDI,
        CodigoMoeda = pais.CodigoMoeda,
        DefaultLocale = pais.DefaultLocale,
        Ativo = pais.Ativo
    };

    /// <summary>
    /// Converte uma entidade Pais para JSON para envio ao Kafka.
    /// </summary>
    public static string ToKafkaJson(this Pais pais) => JsonSerializer.Serialize(new
    {
        pais.Id,
        pais.Nome,
        pais.CodigoISO3,
        pais.CodigoONU,
        pais.CodigoDDI,
        pais.CodigoMoeda,
        pais.DefaultLocale,
        pais.Ativo,
        Timestamp = DateTime.UtcNow
    });

    /// <summary>
    /// Atualiza uma entidade Pais com os dados de UpdatePaisRequest.
    /// </summary>
    public static void UpdateFromRequest(this Pais pais, UpdatePaisRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Nome))
            pais.Nome = request.Nome;

        if (!string.IsNullOrWhiteSpace(request.CodigoISO3))
            pais.CodigoISO3 = request.CodigoISO3;

        if (request.CodigoONU.HasValue)
            pais.CodigoONU = request.CodigoONU.Value;

        if (!string.IsNullOrWhiteSpace(request.CodigoDDI))
            pais.CodigoDDI = request.CodigoDDI;

        if (!string.IsNullOrWhiteSpace(request.CodigoMoeda))
            pais.CodigoMoeda = request.CodigoMoeda;

        if (!string.IsNullOrWhiteSpace(request.DefaultLocale))
            pais.DefaultLocale = request.DefaultLocale;
    }
}

