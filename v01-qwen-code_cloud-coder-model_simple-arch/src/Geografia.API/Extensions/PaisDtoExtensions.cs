using Geografia.API.Domain;
using Geografia.API.DTOs;

namespace Geografia.API.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Pais.
/// </summary>
public static class PaisDtoExtensions
{
    /// <summary>
    /// Converte um DTO de criação para entidade Pais.
    /// </summary>
    public static Pais ToPais(this PaisCreateDto dto) =>
        new()
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
    /// Converte um DTO de atualização para entidade Pais.
    /// </summary>
    public static void UpdateFrom(this Pais entity, PaisUpdateDto dto)
    {
        entity.Nome = dto.Nome;
        entity.CodigoISO3 = dto.CodigoISO3;
        entity.CodigoONU = dto.CodigoONU;
        entity.CodigoDDI = dto.CodigoDDI;
        entity.CodigoMoeda = dto.CodigoMoeda;
        entity.DefaultLocale = dto.DefaultLocale;
    }

    /// <summary>
    /// Converte uma entidade Pais para DTO de resposta.
    /// </summary>
    public static PaisResponseDto ToResponseDto(this Pais entity) =>
        new()
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
}
