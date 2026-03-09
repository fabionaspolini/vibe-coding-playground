using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Xunit;

namespace Geografia.Tests.Extensions;

/// <summary>
/// Testes unitários para <see cref="CidadeDtoExtensions"/>.
/// </summary>
public class CidadeDtoExtensionsTests
{
    /// <summary>
    /// Testa se ToCidade converte corretamente CreateCidadeRequest para entidade Cidade.
    /// </summary>
    [Fact]
    public void ToCidade_DeveConverterCorretamente()
    {
        // Arrange
        var request = new CreateCidadeRequest
        {
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m
        };

        // Act
        var cidade = request.ToCidade();

        // Assert
        Assert.NotNull(cidade);
        Assert.NotEqual(Guid.Empty, cidade.Id);
        Assert.Equal("BR-SP", cidade.EstadoId);
        Assert.Equal("São Paulo", cidade.Nome);
        Assert.Equal("01000-000", cidade.CodigoPostal);
        Assert.Equal(-23.550520m, cidade.Latitude);
        Assert.Equal(-46.633308m, cidade.Longitude);
        Assert.True(cidade.Ativo);
    }

    /// <summary>
    /// Testa se ApplyTo atualiza corretamente a entidade Cidade.
    /// </summary>
    [Fact]
    public void ApplyTo_DeveAtualizarCidadeCorretamente()
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

        var request = new UpdateCidadeRequest
        {
            Nome = "São Paulo Atualizada",
            CodigoPostal = "01310-100",
            Latitude = -23.561684m,
            Longitude = -46.655981m
        };

        // Act
        request.ApplyTo(cidade);

        // Assert
        Assert.Equal("São Paulo Atualizada", cidade.Nome);
        Assert.Equal("01310-100", cidade.CodigoPostal);
    }

    /// <summary>
    /// Testa se ToResponse converte corretamente entidade Cidade para CidadeResponse.
    /// </summary>
    [Fact]
    public void ToResponse_DeveConverterCorretamente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cidade = new Cidade
        {
            Id = id,
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01000-000",
            Latitude = -23.550520m,
            Longitude = -46.633308m,
            Ativo = true
        };

        // Act
        var response = cidade.ToResponse();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(id, response.Id);
        Assert.Equal("BR-SP", response.EstadoId);
        Assert.Equal("São Paulo", response.Nome);
        Assert.Equal("01000-000", response.CodigoPostal);
        Assert.Equal(-23.550520m, response.Latitude);
        Assert.Equal(-46.633308m, response.Longitude);
        Assert.True(response.Ativo);
    }
}
