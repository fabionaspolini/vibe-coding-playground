using Geografia.API.Domain;
using Geografia.API.DTOs;

namespace Geografia.API.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Cidade.
/// </summary>
public static class CidadeDtoExtensions
{
    /// <summary>
    /// Converte um DTO de criação para entidade Cidade.
    /// </summary>
    public static Cidade ToCidade(this CidadeCreateDto dto) =>
        new()
        {
            Id = Guid.NewGuid(),
            EstadoId = dto.EstadoId,
            Nome = dto.Nome,
            CodigoPostal = dto.CodigoPostal,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Ativo = true
        };

    /// <summary>
    /// Converte um DTO de atualização para entidade Cidade.
    /// </summary>
    public static void UpdateFrom(this Cidade entity, CidadeUpdateDto dto)
    {
        entity.Nome = dto.Nome;
        entity.CodigoPostal = dto.CodigoPostal;
        entity.Latitude = dto.Latitude;
        entity.Longitude = dto.Longitude;
    }

    /// <summary>
    /// Converte uma entidade Cidade para DTO de resposta.
    /// </summary>
    public static CidadeResponseDto ToResponseDto(this Cidade entity) =>
        new()
        {
            Id = entity.Id,
            EstadoId = entity.EstadoId,
            Nome = entity.Nome,
            CodigoPostal = entity.CodigoPostal,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            Ativo = entity.Ativo
        };
}
