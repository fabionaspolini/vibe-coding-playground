using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Xunit;

namespace Geografia.Tests;

/// <summary>
/// Testes unitários para as extensões de DTO da entidade Estado.
/// </summary>
public class EstadoDtoExtensionsTests
{
    [Fact]
    public void ToEstado_DeveConverterCorretamente()
    {
        // Arrange
        var dto = new EstadoCreateDto
        {
            Id = "BR-SP",
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = "STATE"
        };

        // Act
        var estado = dto.ToEstado();

        // Assert
        Assert.Equal("BR-SP", estado.Id);
        Assert.Equal("BR", estado.PaisId);
        Assert.Equal("São Paulo", estado.Nome);
        Assert.Equal("SP", estado.Sigla);
        Assert.Equal(TipoEstado.STATE, estado.Tipo);
        Assert.True(estado.Ativo);
    }

    [Fact]
    public void ToEstado_DeveConverterTipoProvince()
    {
        // Arrange
        var dto = new EstadoCreateDto
        {
            Id = "CA-ON",
            PaisId = "CA",
            Nome = "Ontario",
            Sigla = "ON",
            Tipo = "PROVINCE"
        };

        // Act
        var estado = dto.ToEstado();

        // Assert
        Assert.Equal(TipoEstado.PROVINCE, estado.Tipo);
    }

    [Fact]
    public void UpdateFrom_DeveAtualizarCorretamente()
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

        var dto = new EstadoUpdateDto
        {
            Nome = "São Paulo Atualizado",
            Sigla = "SP",
            Tipo = "STATE"
        };

        // Act
        estado.UpdateFrom(dto);

        // Assert
        Assert.Equal("São Paulo Atualizado", estado.Nome);
    }

    [Fact]
    public void ToResponseDto_DeveConverterCorretamente()
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
        var response = estado.ToResponseDto();

        // Assert
        Assert.Equal("BR-SP", response.Id);
        Assert.Equal("BR", response.PaisId);
        Assert.Equal("São Paulo", response.Nome);
        Assert.Equal("SP", response.Sigla);
        Assert.Equal("STATE", response.Tipo);
        Assert.True(response.Ativo);
    }
}
