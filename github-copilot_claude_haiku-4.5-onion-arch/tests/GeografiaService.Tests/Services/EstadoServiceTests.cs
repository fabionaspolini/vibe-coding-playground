using FakeItEasy;
using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using GeografiaService.Domain.Entities;
using GeografiaService.Domain.Enums;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Xunit;

namespace GeografiaService.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de Estado.
/// </summary>
public class EstadoServiceTests
{
    private readonly IEstadoRepository _fakeRepository;
    private readonly IEventProducer _fakeEventProducer;
    private readonly ILogger<EstadoService> _fakeLogger;
    private readonly EstadoService _sut;

    public EstadoServiceTests()
    {
        _fakeRepository = A.Fake<IEstadoRepository>();
        _fakeEventProducer = A.Fake<IEventProducer>();
        _fakeLogger = A.Fake<ILogger<EstadoService>>();
        _sut = new EstadoService(_fakeRepository, _fakeEventProducer, _fakeLogger);
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnEstadoResponse()
    {
        // Arrange
        var request = new CreateEstadoRequest
        {
            Id = "BR-SP",
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = EstadoTipo.State
        };

        A.CallTo(() => _fakeRepository.AddAsync(A<Estado>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR-SP", result.Id);
        Assert.Equal("São Paulo", result.Nome);
        Assert.True(result.Ativo);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnEstadoResponse()
    {
        // Arrange
        var estadoId = "BR-SP";
        var estado = new Estado
        {
            Id = estadoId,
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = EstadoTipo.State,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(estadoId, A<CancellationToken>._))
            .Returns(Task.FromResult<Estado?>(estado));

        // Act
        var result = await _sut.GetByIdAsync(estadoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR-SP", result.Id);
        Assert.Equal("São Paulo", result.Nome);
    }

    [Fact]
    public async Task ListAsync_WithPaisIdFilter_ShouldReturnEstadosFromPais()
    {
        // Arrange
        var paisId = "BR";
        var estados = new List<Estado>
        {
            new() { Id = "BR-SP", PaisId = paisId, Nome = "São Paulo", Sigla = "SP", Tipo = EstadoTipo.State, Ativo = true },
            new() { Id = "BR-SC", PaisId = paisId, Nome = "Santa Catarina", Sigla = "SC", Tipo = EstadoTipo.State, Ativo = true }
        };

        A.CallTo(() => _fakeRepository.GetAllAsync(A<CancellationToken>._))
            .Returns(Task.FromResult<IEnumerable<Estado>>(estados));

        // Act
        var result = await _sut.ListAsync(paisId: paisId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, e => Assert.Equal(paisId, e.PaisId));
    }

    [Fact]
    public async Task UpdateAsync_WithValidRequest_ShouldUpdateEstado()
    {
        // Arrange
        var estadoId = "BR-SP";
        var updateRequest = new UpdateEstadoRequest { Nome = "São Paulo Atualizado" };
        var estado = new Estado
        {
            Id = estadoId,
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = EstadoTipo.State,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(estadoId, A<CancellationToken>._))
            .Returns(Task.FromResult<Estado?>(estado));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Estado>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.UpdateAsync(estadoId, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("São Paulo Atualizado", result.Nome);
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_ShouldSoftDeleteEstado()
    {
        // Arrange
        var estadoId = "BR-SP";
        var estado = new Estado
        {
            Id = estadoId,
            PaisId = "BR",
            Nome = "São Paulo",
            Sigla = "SP",
            Tipo = EstadoTipo.State,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(estadoId, A<CancellationToken>._))
            .Returns(Task.FromResult<Estado?>(estado));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Estado>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.RemoveAsync(estadoId);

        // Assert
        Assert.True(result);
        Assert.False(estado.Ativo);
    }
}

