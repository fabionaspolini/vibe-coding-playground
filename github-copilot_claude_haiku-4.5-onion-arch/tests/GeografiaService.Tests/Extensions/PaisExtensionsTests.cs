using GeografiaService.Application.DTOs;
using GeografiaService.Application.Extensions;
using GeografiaService.Domain.Entities;
using Xunit;

namespace GeografiaService.Tests.Extensions;

/// <summary>
/// Testes unitários para os extension methods de País.
/// </summary>
public class PaisExtensionsTests
{
    [Fact]
    public void ToPais_WithValidRequest_ShouldConvertCorrectly()
    {
        // Arrange
        var request = new CreatePaisRequest
        {
            Id = "BR",
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR"
        };

        // Act
        var result = request.ToPais();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
        Assert.Equal("BRA", result.CodigoISO3);
    }

    [Fact]
    public void ToPaisResponse_WithValidEntity_ShouldConvertCorrectly()
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
        var result = pais.ToPaisResponse();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
        Assert.True(result.Ativo);
    }

    [Fact]
    public void ToKafkaJson_WithValidEntity_ShouldReturnJsonString()
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
        var result = pais.ToKafkaJson();

        // Assert
        Assert.NotNull(result);
        Assert.Contains("BR", result);
        Assert.Contains("Brasil", result);
    }

    [Fact]
    public void UpdateFromRequest_WithValidRequest_ShouldUpdateEntity()
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
        var request = new UpdatePaisRequest
        {
            Nome = "Brasil Atualizado",
            CodigoISO3 = "BRA"
        };

        // Act
        pais.UpdateFromRequest(request);

        // Assert
        Assert.Equal("Brasil Atualizado", pais.Nome);
        Assert.Equal("BRA", pais.CodigoISO3);
    }
}

