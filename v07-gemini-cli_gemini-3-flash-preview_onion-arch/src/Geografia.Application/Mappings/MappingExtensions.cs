using Geografia.Application.Dtos;
using Geografia.Domain.Entities;

namespace Geografia.Application.Mappings;

public static class MappingExtensions
{
    public static PaisDto ToDto(this Pais entity) => new()
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

    public static Pais ToEntity(this PaisRequest request) => new()
    {
        Id = request.Id,
        Nome = request.Nome,
        CodigoISO3 = request.CodigoISO3,
        CodigoONU = request.CodigoONU,
        CodigoDDI = request.CodigoDDI,
        CodigoMoeda = request.CodigoMoeda,
        DefaultLocale = request.DefaultLocale,
        Ativo = true
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

    public static EstadoDto ToDto(this Estado entity) => new()
    {
        Id = entity.Id,
        PaisId = entity.PaisId,
        Nome = entity.Nome,
        Sigla = entity.Sigla,
        Tipo = entity.Tipo,
        Ativo = entity.Ativo
    };

    public static Estado ToEntity(this EstadoRequest request) => new()
    {
        Id = request.Id,
        PaisId = request.PaisId,
        Nome = request.Nome,
        Sigla = request.Sigla,
        Tipo = request.Tipo,
        Ativo = true
    };

    public static void UpdateFromRequest(this Estado entity, EstadoRequest request)
    {
        entity.Nome = request.Nome;
        entity.Sigla = request.Sigla;
        entity.Tipo = request.Tipo;
        entity.PaisId = request.PaisId;
    }

    public static CidadeDto ToDto(this Cidade entity) => new()
    {
        Id = entity.Id,
        EstadoId = entity.EstadoId,
        Nome = entity.Nome,
        CodigoPostal = entity.CodigoPostal,
        Latitude = entity.Latitude,
        Longitude = entity.Longitude,
        Ativo = entity.Ativo
    };

    public static Cidade ToEntity(this CidadeRequest request) => new()
    {
        Id = Guid.NewGuid(),
        EstadoId = request.EstadoId,
        Nome = request.Nome,
        CodigoPostal = request.CodigoPostal,
        Latitude = request.Latitude,
        Longitude = request.Longitude,
        Ativo = true
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
