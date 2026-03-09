namespace Geografia.Application.Extensions;

using Geografia.Application.DTOs;
using Geografia.Domain.Entities;

/// <summary>
/// Extension methods para conversão de Pais.
/// </summary>
public static class PaisExtensions
{
    /// <summary>
    /// Converte uma entidade Pais para PaisDto.
    /// </summary>
    /// <param name="pais">Entidade Pais.</param>
    /// <returns>DTO de Pais.</returns>
    public static PaisDto ToDto(this Pais pais) => new()
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
    /// Converte um CriarPaisDto para uma entidade Pais.
    /// </summary>
    /// <param name="dto">DTO para criar Pais.</param>
    /// <returns>Entidade Pais.</returns>
    public static Pais ToPais(this CriarPaisDto dto) => new()
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
    /// Atualiza uma entidade Pais com valores de um AtualizarPaisDto.
    /// </summary>
    /// <param name="pais">Entidade Pais a atualizar.</param>
    /// <param name="dto">DTO para atualizar Pais.</param>
    public static void UpdateFromDto(this Pais pais, AtualizarPaisDto dto)
    {
        pais.Nome = dto.Nome;
        pais.CodigoISO3 = dto.CodigoISO3;
        pais.CodigoONU = dto.CodigoONU;
        pais.CodigoDDI = dto.CodigoDDI;
        pais.CodigoMoeda = dto.CodigoMoeda;
        pais.DefaultLocale = dto.DefaultLocale;
    }
}

/// <summary>
/// Extension methods para conversão de Estado.
/// </summary>
public static class EstadoExtensions
{
    /// <summary>
    /// Converte uma entidade Estado para EstadoDto.
    /// </summary>
    /// <param name="estado">Entidade Estado.</param>
    /// <returns>DTO de Estado.</returns>
    public static EstadoDto ToDto(this Estado estado) => new()
    {
        Id = estado.Id,
        PaisId = estado.PaisId,
        Nome = estado.Nome,
        Sigla = estado.Sigla,
        Tipo = estado.Tipo.ToString(),
        Ativo = estado.Ativo
    };

    /// <summary>
    /// Converte um CriarEstadoDto para uma entidade Estado.
    /// </summary>
    /// <param name="dto">DTO para criar Estado.</param>
    /// <returns>Entidade Estado.</returns>
    public static Estado ToEstado(this CriarEstadoDto dto) => new()
    {
        Id = dto.Id,
        PaisId = dto.PaisId,
        Nome = dto.Nome,
        Sigla = dto.Sigla,
        Tipo = Enum.Parse<TipoEstado>(dto.Tipo, ignoreCase: true),
        Ativo = true
    };

    /// <summary>
    /// Atualiza uma entidade Estado com valores de um AtualizarEstadoDto.
    /// </summary>
    /// <param name="estado">Entidade Estado a atualizar.</param>
    /// <param name="dto">DTO para atualizar Estado.</param>
    public static void UpdateFromDto(this Estado estado, AtualizarEstadoDto dto)
    {
        estado.Nome = dto.Nome;
        estado.Sigla = dto.Sigla;
        estado.Tipo = Enum.Parse<TipoEstado>(dto.Tipo, ignoreCase: true);
    }
}

/// <summary>
/// Extension methods para conversão de Cidade.
/// </summary>
public static class CidadeExtensions
{
    /// <summary>
    /// Converte uma entidade Cidade para CidadeDto.
    /// </summary>
    /// <param name="cidade">Entidade Cidade.</param>
    /// <returns>DTO de Cidade.</returns>
    public static CidadeDto ToDto(this Cidade cidade) => new()
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
    /// Converte um CriarCidadeDto para uma entidade Cidade.
    /// </summary>
    /// <param name="dto">DTO para criar Cidade.</param>
    /// <returns>Entidade Cidade.</returns>
    public static Cidade ToCidade(this CriarCidadeDto dto) => new()
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
    /// Atualiza uma entidade Cidade com valores de um AtualizarCidadeDto.
    /// </summary>
    /// <param name="cidade">Entidade Cidade a atualizar.</param>
    /// <param name="dto">DTO para atualizar Cidade.</param>
    public static void UpdateFromDto(this Cidade cidade, AtualizarCidadeDto dto)
    {
        cidade.Nome = dto.Nome;
        cidade.CodigoPostal = dto.CodigoPostal;
        cidade.Latitude = dto.Latitude;
        cidade.Longitude = dto.Longitude;
    }
}

