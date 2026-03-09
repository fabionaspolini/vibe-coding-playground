using Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Estado.
/// </summary>
public static class EstadoDtoExtensions
{
    /// <summary>
    /// Converte CreateEstadoRequest para entidade Estado.
    /// </summary>
    public static Estado ToEstado(this CreateEstadoRequest request) =>
        new()
        {
            Id = request.Id.ToUpperInvariant(),
            PaisId = request.PaisId.ToUpperInvariant(),
            Nome = request.Nome,
            Sigla = request.Sigla.ToUpperInvariant(),
            Tipo = ParseSubdivisionType(request.Tipo),
            Ativo = true
        };

    /// <summary>
    /// Converte UpdateEstadoRequest para entidade Estado.
    /// </summary>
    public static void ApplyTo(this UpdateEstadoRequest request, Estado estado)
    {
        estado.Nome = request.Nome;
        estado.Sigla = request.Sigla.ToUpperInvariant();
        estado.Tipo = ParseSubdivisionType(request.Tipo);
    }

    /// <summary>
    /// Converte entidade Estado para EstadoResponse.
    /// </summary>
    public static EstadoResponse ToResponse(this Estado estado) =>
        new()
        {
            Id = estado.Id,
            PaisId = estado.PaisId,
            Nome = estado.Nome,
            Sigla = estado.Sigla,
            Tipo = estado.Tipo,
            Ativo = estado.Ativo
        };

    private static SubdivisionType ParseSubdivisionType(string tipo) =>
        Enum.TryParse<SubdivisionType>(tipo, true, out var result)
            ? result
            : SubdivisionType.State;
}
