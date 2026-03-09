using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Xunit;

namespace Geografia.Tests;

/// <summary>
/// Testes unitários para as extensões de DTO da entidade Pais.
/// </summary>
public class PaisDtoExtensionsTests
{
    [Fact]
    public void ToPais_DeveConverterCorretamente()
    {
        // Arrange
        var dto = new PaisCreateDto
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
        var pais = dto.ToPais();

        // Assert
        Assert.Equal("BR", pais.Id);
        Assert.Equal("Brasil", pais.Nome);
        Assert.Equal("BRA", pais.CodigoISO3);
        Assert.Equal(76, pais.CodigoONU);
        Assert.Equal("+55", pais.CodigoDDI);
        Assert.Equal("BRL", pais.CodigoMoeda);
        Assert.Equal("pt-BR", pais.DefaultLocale);
        Assert.True(pais.Ativo);
    }

    [Fact]
    public void UpdateFrom_DeveAtualizarCorretamente()
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

        var dto = new PaisUpdateDto
        {
            Nome = "Brasil Atualizado",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR"
        };

        // Act
        pais.UpdateFrom(dto);

        // Assert
        Assert.Equal("Brasil Atualizado", pais.Nome);
    }

    [Fact]
    public void ToResponseDto_DeveConverterCorretamente()
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
        var response = pais.ToResponseDto();

        // Assert
        Assert.Equal("BR", response.Id);
        Assert.Equal("Brasil", response.Nome);
        Assert.True(response.Ativo);
    }
}
