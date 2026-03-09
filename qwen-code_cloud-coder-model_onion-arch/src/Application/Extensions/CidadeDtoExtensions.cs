using Application.DTOs;
using Domain.Entities;

namespace Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Cidade.
/// </summary>
public static class CidadeDtoExtensions
{
    /// <summary>
    /// Converte CreateCidadeRequest para entidade Cidade.
    /// </summary>
    public static Cidade ToCidade(this CreateCidadeRequest request) =>
        new()
        {
            Id = Guid.NewGuid(),
            EstadoId = request.EstadoId.ToUpperInvariant(),
            Nome = request.Nome,
            CodigoPostal = request.CodigoPostal,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Ativo = true
        };

    /// <summary>
    /// Converte UpdateCidadeRequest para entidade Cidade.
    /// </summary>
    public static void ApplyTo(this UpdateCidadeRequest request, Cidade cidade)
    {
        cidade.Nome = request.Nome;
        cidade.CodigoPostal = request.CodigoPostal;
        cidade.Latitude = request.Latitude;
        cidade.Longitude = request.Longitude;
    }

    /// <summary>
    /// Converte entidade Cidade para CidadeResponse.
    /// </summary>
    public static CidadeResponse ToResponse(this Cidade cidade) =>
        new()
        {
            Id = cidade.Id,
            EstadoId = cidade.EstadoId,
            Nome = cidade.Nome,
            CodigoPostal = cidade.CodigoPostal,
            Latitude = cidade.Latitude,
            Longitude = cidade.Longitude,
            Ativo = cidade.Ativo
        };
}
