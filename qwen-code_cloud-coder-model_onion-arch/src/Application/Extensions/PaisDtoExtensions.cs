using Application.DTOs;
using Domain.Entities;

namespace Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Pais.
/// </summary>
public static class PaisDtoExtensions
{
    /// <summary>
    /// Converte CreatePaisRequest para entidade Pais.
    /// </summary>
    public static Pais ToPais(this CreatePaisRequest request) =>
        new()
        {
            Id = request.Id.ToUpperInvariant(),
            Nome = request.Nome,
            CodigoISO3 = request.CodigoISO3.ToUpperInvariant(),
            CodigoONU = request.CodigoONU,
            CodigoDDI = request.CodigoDDI,
            CodigoMoeda = request.CodigoMoeda.ToUpperInvariant(),
            DefaultLocale = request.DefaultLocale,
            Ativo = true
        };

    /// <summary>
    /// Converte UpdatePaisRequest para entidade Pais.
    /// </summary>
    public static void ApplyTo(this UpdatePaisRequest request, Pais pais)
    {
        pais.Nome = request.Nome;
        pais.CodigoISO3 = request.CodigoISO3.ToUpperInvariant();
        pais.CodigoONU = request.CodigoONU;
        pais.CodigoDDI = request.CodigoDDI;
        pais.CodigoMoeda = request.CodigoMoeda.ToUpperInvariant();
        pais.DefaultLocale = request.DefaultLocale;
    }

    /// <summary>
    /// Converte entidade Pais para PaisResponse.
    /// </summary>
    public static PaisResponse ToResponse(this Pais pais) =>
        new()
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
}
