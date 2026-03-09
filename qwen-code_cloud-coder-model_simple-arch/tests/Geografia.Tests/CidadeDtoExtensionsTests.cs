using Geografia.API.Domain;
using Geografia.API.DTOs;
using Geografia.API.Extensions;
using Xunit;

namespace Geografia.Tests;

/// <summary>
/// Testes unitários para as extensões de DTO da entidade Cidade.
/// </summary>
public class CidadeDtoExtensionsTests
{
    [Fact]
    public void ToCidade_DeveConverterCorretamente()
    {
        // Arrange
        var dto = new CidadeCreateDto
        {
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m
        };

        // Act
        var cidade = dto.ToCidade();

        // Assert
        Assert.Equal("BR-SP", cidade.EstadoId);
        Assert.Equal("São Paulo", cidade.Nome);
        Assert.Equal("01000-000", cidade.CodigoPostal);
        Assert.Equal(-23.550520m, cidade.Latitude);
        Assert.Equal(-46.633308m, cidade.Longitude);
        Assert.True(cidade.Ativo);
        Assert.NotEqual(Guid.Empty, cidade.Id);
    }

    [Fact]
    public void UpdateFrom_DeveAtualizarCorretamente()
    {
        // Arrange
        var cidade = new Cidade
        {
            Id = Guid.NewGuid(),
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m,
            Ativo = true
        };

        var dto = new CidadeUpdateDto
        {
            Nome = "São Paulo Atualizado",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m
        };

        // Act
        cidade.UpdateFrom(dto);

        // Assert
        Assert.Equal("São Paulo Atualizado", cidade.Nome);
    }

    [Fact]
    public void ToResponseDto_DeveConverterCorretamente()
    {
        // Arrange
        var cidade = new Cidade
        {
            Id = Guid.NewGuid(),
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m,
            Ativo = true
        };

        // Act
        var response = cidade.ToResponseDto();

        // Assert
        Assert.Equal(cidade.Id, response.Id);
        Assert.Equal("BR-SP", response.EstadoId);
        Assert.Equal("São Paulo", response.Nome);
        Assert.True(response.Ativo);
    }
}
