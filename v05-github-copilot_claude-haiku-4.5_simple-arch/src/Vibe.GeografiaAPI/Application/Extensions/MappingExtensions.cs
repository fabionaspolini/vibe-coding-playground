using Vibe.GeografiaAPI.Application.DTOs;
using Vibe.GeografiaAPI.Domain.Entities;

namespace Vibe.GeografiaAPI.Application.Extensions;

/// <summary>
/// Métodos de extensão para mapeamento entre entidades de domínio e DTOs.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Converte uma entidade Pais para um DTO de leitura.
    /// </summary>
    public static PaisDto ToPaisDto(this Pais pais) => new()
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
    /// Converte um DTO de criação para uma entidade Pais.
    /// </summary>
    public static Pais ToPaisEntity(this CreatePaisDto dto) => new()
    {
        Id = dto.Id,
        Nome = dto.Nome,
        CodigoISO3 = dto.CodigoISO3,
        CodigoONU = dto.CodigoONU,
        CodigoDDI = dto.CodigoDDI,
        CodigoMoeda = dto.CodigoMoeda,
        DefaultLocale = dto.DefaultLocale,
        Ativo = true
    };

    /// <summary>
    /// Aplica atualizações de um DTO a uma entidade Pais existente.
    /// </summary>
    public static void UpdateFromDto(this Pais pais, UpdatePaisDto dto)
    {
        if (!string.IsNullOrEmpty(dto.Nome))
            pais.Nome = dto.Nome;
        if (!string.IsNullOrEmpty(dto.CodigoISO3))
            pais.CodigoISO3 = dto.CodigoISO3;
        if (dto.CodigoONU.HasValue)
            pais.CodigoONU = dto.CodigoONU.Value;
        if (!string.IsNullOrEmpty(dto.CodigoDDI))
            pais.CodigoDDI = dto.CodigoDDI;
        if (!string.IsNullOrEmpty(dto.CodigoMoeda))
            pais.CodigoMoeda = dto.CodigoMoeda;
        if (!string.IsNullOrEmpty(dto.DefaultLocale))
            pais.DefaultLocale = dto.DefaultLocale;
    }

    /// <summary>
    /// Converte uma entidade Estado para um DTO de leitura.
    /// </summary>
    public static EstadoDto ToEstadoDto(this Estado estado) => new()
    {
        Id = estado.Id,
        PaisId = estado.PaisId,
        Nome = estado.Nome,
        Sigla = estado.Sigla,
        Tipo = estado.Tipo,
        Ativo = estado.Ativo
    };

    /// <summary>
    /// Converte um DTO de criação para uma entidade Estado.
    /// </summary>
    public static Estado ToEstadoEntity(this CreateEstadoDto dto) => new()
    {
        Id = dto.Id,
        PaisId = dto.PaisId,
        Nome = dto.Nome,
        Sigla = dto.Sigla,
        Tipo = dto.Tipo,
        Ativo = true
    };

    /// <summary>
    /// Aplica atualizações de um DTO a uma entidade Estado existente.
    /// </summary>
    public static void UpdateFromDto(this Estado estado, UpdateEstadoDto dto)
    {
        if (!string.IsNullOrEmpty(dto.Nome))
            estado.Nome = dto.Nome;
        if (!string.IsNullOrEmpty(dto.Sigla))
            estado.Sigla = dto.Sigla;
        if (dto.Tipo.HasValue)
            estado.Tipo = dto.Tipo.Value;
    }

    /// <summary>
    /// Converte uma entidade Cidade para um DTO de leitura.
    /// </summary>
    public static CidadeDto ToCidadeDto(this Cidade cidade) => new()
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
    /// Converte um DTO de criação para uma entidade Cidade.
    /// </summary>
    public static Cidade ToCidadeEntity(this CreateCidadeDto dto) => new()
    {
        EstadoId = dto.EstadoId,
        Nome = dto.Nome,
        CodigoPostal = dto.CodigoPostal,
        Latitude = dto.Latitude,
        Longitude = dto.Longitude,
        Ativo = true
    };

    /// <summary>
    /// Aplica atualizações de um DTO a uma entidade Cidade existente.
    /// </summary>
    public static void UpdateFromDto(this Cidade cidade, UpdateCidadeDto dto)
    {
        if (!string.IsNullOrEmpty(dto.Nome))
            cidade.Nome = dto.Nome;
        if (!string.IsNullOrEmpty(dto.CodigoPostal))
            cidade.CodigoPostal = dto.CodigoPostal;
        if (dto.Latitude.HasValue)
            cidade.Latitude = dto.Latitude.Value;
        if (dto.Longitude.HasValue)
            cidade.Longitude = dto.Longitude.Value;
    }
}

