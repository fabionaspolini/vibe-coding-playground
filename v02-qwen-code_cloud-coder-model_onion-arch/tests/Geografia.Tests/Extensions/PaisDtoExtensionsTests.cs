using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Xunit;

namespace Geografia.Tests.Extensions;

/// <summary>
/// Testes unitários para <see cref="PaisDtoExtensions"/>.
/// </summary>
public class PaisDtoExtensionsTests
{
    /// <summary>
    /// Testa se ToPais converte corretamente CreatePaisRequest para entidade Pais.
    /// </summary>
    [Fact]
    public void ToPais_DeveConverterCorretamente()
    {
        // Arrange
        var request = new CreatePaisRequest
        {
            Id = "br",
            Nome = "Brasil",
            CodigoISO3 = "bra",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "brl",
            DefaultLocale = "pt-BR"
        };

        // Act
        var pais = request.ToPais();

        // Assert
        Assert.NotNull(pais);
        Assert.Equal("BR", pais.Id);
        Assert.Equal("Brasil", pais.Nome);
        Assert.Equal("BRA", pais.CodigoISO3);
        Assert.Equal(76, pais.CodigoONU);
        Assert.Equal("+55", pais.CodigoDDI);
        Assert.Equal("BRL", pais.CodigoMoeda);
        Assert.Equal("pt-BR", pais.DefaultLocale);
        Assert.True(pais.Ativo);
    }

    /// <summary>
    /// Testa se ApplyTo atualiza corretamente a entidade Pais.
    /// </summary>
    [Fact]
    public void ApplyTo_DeveAtualizarPaisCorretamente()
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
            DefaultLocale = "pt-BR"
        };

        var request = new UpdatePaisRequest
        {
            Nome = "Brasil Atualizado",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR"
        };

        // Act
        request.ApplyTo(pais);

        // Assert
        Assert.Equal("Brasil Atualizado", pais.Nome);
    }

    /// <summary>
    /// Testa se ToResponse converte corretamente entidade Pais para PaisResponse.
    /// </summary>
    [Fact]
    public void ToResponse_DeveConverterCorretamente()
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
        var response = pais.ToResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal("BR", response.Id);
        Assert.Equal("Brasil", response.Nome);
        Assert.Equal("BRA", response.CodigoISO3);
        Assert.Equal(76, response.CodigoONU);
        Assert.Equal("+55", response.CodigoDDI);
        Assert.Equal("BRL", response.CodigoMoeda);
        Assert.Equal("pt-BR", response.DefaultLocale);
        Assert.True(response.Ativo);
    }
}
