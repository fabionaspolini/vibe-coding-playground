using Vibe.GeografiaAPI.Application.DTOs;
using Vibe.GeografiaAPI.Application.Extensions;
using Vibe.GeografiaAPI.Domain.Entities;

namespace Vibe.GeografiaAPI.Tests;

public class MappingExtensionsTests
{
    [Fact]
    public void ToPaisDto_ShouldConvertPaisEntityToDto()
    {
        // Arrange
        var pais = new Pais
        {
            Id = "BR",
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR",
            Ativo = true
        };

        // Act
        var dto = pais.ToPaisDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal("BR", dto.Id);
        Assert.Equal("Brasil", dto.Nome);
        Assert.Equal("BRA", dto.CodigoISO3);
        Assert.Equal(76, dto.CodigoONU);
        Assert.Equal("+55", dto.CodigoDDI);
        Assert.Equal("BRL", dto.CodigoMoeda);
        Assert.Equal("pt-BR", dto.DefaultLocale);
        Assert.True(dto.Ativo);
    }

    [Fact]
    public void ToPaisEntity_ShouldConvertCreatePaisDtoToEntity()
    {
        // Arrange
        var createDto = new CreatePaisDto
        {
            Id = "US",
            Nome = "Estados Unidos",
            CodigoISO3 = "USA",
            CodigoONU = 840,
            CodigoDDI = "+1",
            CodigoMoeda = "USD",
            DefaultLocale = "en-US"
        };

        // Act
        var entity = createDto.ToPaisEntity();

        // Assert
        Assert.NotNull(entity);
        Assert.Equal("US", entity.Id);
        Assert.Equal("Estados Unidos", entity.Nome);
        Assert.Equal("USA", entity.CodigoISO3);
        Assert.Equal(840, entity.CodigoONU);
        Assert.Equal("+1", entity.CodigoDDI);
        Assert.Equal("USD", entity.CodigoMoeda);
        Assert.Equal("en-US", entity.DefaultLocale);
        Assert.True(entity.Ativo);
    }

    [Fact]
    public void ToEstadoDto_ShouldConvertEstadoEntityToDto()
    {
        // Arrange
        var estado = new Estado
        {
            Id = "BR-SP",
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = TipoEstado.STATE,
            Ativo = true
        };

        // Act
        var dto = estado.ToEstadoDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal("BR-SP", dto.Id);
        Assert.Equal("BR", dto.PaisId);
        Assert.Equal("São Paulo", dto.Nome);
        Assert.Equal("SP", dto.Sigla);
        Assert.Equal(TipoEstado.STATE, dto.Tipo);
        Assert.True(dto.Ativo);
    }

    [Fact]
    public void ToCidadeDto_ShouldConvertCidadeEntityToDto()
    {
        // Arrange
        var cityId = Guid.NewGuid();
        var cidade = new Cidade
        {
            Id = cityId,
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01310-100",
            Latitude = -23.550520m,
            Longitude = -46.633308m,
            Ativo = true
        };

        // Act
        var dto = cidade.ToCidadeDto();

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(cityId, dto.Id);
        Assert.Equal("BR-SP", dto.EstadoId);
        Assert.Equal("São Paulo", dto.Nome);
        Assert.Equal("01310-100", dto.CodigoPostal);
        Assert.Equal(-23.550520m, dto.Latitude);
        Assert.Equal(-46.633308m, dto.Longitude);
        Assert.True(dto.Ativo);
    }

    [Fact]
    public void UpdateFromDto_ShouldUpdatePaisEntity()
    {
        // Arrange
        var pais = new Pais
        {
            Id = "BR",
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR",
            Ativo = true
        };

        var updateDto = new UpdatePaisDto
        {
            Nome = "República Federativa do Brasil",
            DefaultLocale = "pt-BR"
        };

        // Act
        pais.UpdateFromDto(updateDto);

        // Assert
        Assert.Equal("República Federativa do Brasil", pais.Nome);
        Assert.Equal("BRA", pais.CodigoISO3); // Não foi alterado
    }
}

