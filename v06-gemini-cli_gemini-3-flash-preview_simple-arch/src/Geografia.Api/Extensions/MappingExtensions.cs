using Geografia.Api.Domain.Entities;
using Geografia.Api.Dtos;

namespace Geografia.Api.Extensions;

/// <summary>
/// Métodos de extensão para conversão entre entidades e DTOs.
/// </summary>
public static class MappingExtensions
{
    // Pais
    public static PaisResponse ToResponse(this Pais entity) => new()
    {
        Id = entity.Id,
        Nome = entity.Nome,
        CodigoISO3 = entity.CodigoISO3,
        CodigoONU = entity.CodigoONU,
        CodigoDDI = entity.CodigoDDI,
        CodigoMoeda = entity.CodigoMoeda,
        DefaultLocale = entity.DefaultLocale,
        Ativo = entity.Ativo
    };

    public static void UpdateFromRequest(this Pais entity, PaisRequest request)
    {
        entity.Nome = request.Nome;
        entity.CodigoISO3 = request.CodigoISO3;
        entity.CodigoONU = request.CodigoONU;
        entity.CodigoDDI = request.CodigoDDI;
        entity.CodigoMoeda = request.CodigoMoeda;
        entity.DefaultLocale = request.DefaultLocale;
    }

    // Estado
    public static EstadoResponse ToResponse(this Estado entity) => new()
    {
        Id = entity.Id,
        PaisId = entity.PaisId,
        Nome = entity.Nome,
        Sigla = entity.Sigla,
        Tipo = entity.Tipo,
        Ativo = entity.Ativo
    };

    public static void UpdateFromRequest(this Estado entity, EstadoRequest request)
    {
        entity.PaisId = request.PaisId;
        entity.Nome = request.Nome;
        entity.Sigla = request.Sigla;
        entity.Tipo = request.Tipo;
    }

    // Cidade
    public static CidadeResponse ToResponse(this Cidade entity) => new()
    {
        Id = entity.Id,
        EstadoId = entity.EstadoId,
        Nome = entity.Nome,
        CodigoPostal = entity.CodigoPostal,
        Latitude = entity.Latitude,
        Longitude = entity.Longitude,
        Ativo = entity.Ativo
    };

    public static void UpdateFromRequest(this Cidade entity, CidadeRequest request)
    {
        entity.EstadoId = request.EstadoId;
        entity.Nome = request.Nome;
        entity.CodigoPostal = request.CodigoPostal;
        entity.Latitude = request.Latitude;
        entity.Longitude = request.Longitude;
    }
}
