using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace Geografia.Tests.Extensions;

/// <summary>
/// Testes unitários para <see cref="EstadoDtoExtensions"/>.
/// </summary>
public class EstadoDtoExtensionsTests
{
    /// <summary>
    /// Testa se ToEstado converte corretamente CreateEstadoRequest para entidade Estado.
    /// </summary>
    [Fact]
    public void ToEstado_DeveConverterCorretamente()
    {
        // Arrange
        var request = new CreateEstadoRequest
        {
            Id = "br-sp",
            PaisId = "br",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = "STATE"
        };

        // Act
        var estado = request.ToEstado();

        // Assert
        Assert.NotNull(estado);
        Assert.Equal("BR-SP", estado.Id);
        Assert.Equal("BR", estado.PaisId);
        Assert.Equal("São Paulo", estado.Nome);
        Assert.Equal("SP", estado.Sigla);
        Assert.Equal(SubdivisionType.State, estado.Tipo);
        Assert.True(estado.Ativo);
    }

    /// <summary>
    /// Testa se ToEstado converte tipo de estado corretamente (case insensitive).
    /// </summary>
    [Fact]
    public void ToEstado_DeveConverterTipoCaseInsensitive()
    {
        // Arrange
        var request = new CreateEstadoRequest
        {
            Id = "ca-on",
            PaisId = "CA",
            Nome = "Ontario",
            Sigla = "ON",
            Tipo = "province"
        };

        // Act
        var estado = request.ToEstado();

        // Assert
        Assert.Equal(SubdivisionType.Province, estado.Tipo);
    }

    /// <summary>
    /// Testa se ToResponse converte corretamente entidade Estado para EstadoResponse.
    /// </summary>
    [Fact]
    public void ToResponse_DeveConverterCorretamente()
    {
        // Arrange
        var estado = new Estado
        {
            Id = "BR-SP",
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = SubdivisionType.State,
            Ativo = true
        };

        // Act
        var response = estado.ToResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal("BR-SP", response.Id);
        Assert.Equal("BR", response.PaisId);
        Assert.Equal("São Paulo", response.Nome);
        Assert.Equal("SP", response.Sigla);
        Assert.Equal(SubdivisionType.State, response.Tipo);
        Assert.True(response.Ativo);
    }
}
