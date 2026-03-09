using Geografia.API.Domain;
using Geografia.API.DTOs;

namespace Geografia.API.Extensions;

/// <summary>
/// Extension methods para conversão de DTOs da entidade Estado.
/// </summary>
public static class EstadoDtoExtensions
{
    /// <summary>
    /// Converte um DTO de criação para entidade Estado.
    /// </summary>
    public static Estado ToEstado(this EstadoCreateDto dto) =>
        new()
        {
            Id = dto.Id,
            PaisId = dto.PaisId,
            Nome = dto.Nome,
            Sigla = dto.Sigla,
            Tipo = ParseTipo(dto.Tipo),
            Ativo = true
        };

    /// <summary>
    /// Converte um DTO de atualização para entidade Estado.
    /// </summary>
    public static void UpdateFrom(this Estado entity, EstadoUpdateDto dto)
    {
        entity.Nome = dto.Nome;
        entity.Sigla = dto.Sigla;
        entity.Tipo = ParseTipo(dto.Tipo);
    }

    /// <summary>
    /// Converte uma entidade Estado para DTO de resposta.
    /// </summary>
    public static EstadoResponseDto ToResponseDto(this Estado entity) =>
        new()
        {
            Id = entity.Id,
            PaisId = entity.PaisId,
            Nome = entity.Nome,
            Sigla = entity.Sigla,
            Tipo = entity.Tipo.ToString(),
            Ativo = entity.Ativo
        };

    private static TipoEstado ParseTipo(string tipo) =>
        Enum.TryParse<TipoEstado>(tipo.ToUpper(), true, out var result) ? result : TipoEstado.STATE;
}
