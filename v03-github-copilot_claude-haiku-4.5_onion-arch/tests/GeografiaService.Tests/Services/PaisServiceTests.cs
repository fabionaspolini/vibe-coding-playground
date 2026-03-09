using FakeItEasy;
using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Xunit;

namespace GeografiaService.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de País.
/// </summary>
public class PaisServiceTests
{
    private readonly IPaisRepository _fakeRepository;
    private readonly IEventProducer _fakeEventProducer;
    private readonly ILogger<PaisService> _fakeLogger;
    private readonly PaisService _sut;

    public PaisServiceTests()
    {
        _fakeRepository = A.Fake<IPaisRepository>();
        _fakeEventProducer = A.Fake<IEventProducer>();
        _fakeLogger = A.Fake<ILogger<PaisService>>();
        _sut = new PaisService(_fakeRepository, _fakeEventProducer, _fakeLogger);
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnPaisResponse()
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

        A.CallTo(() => _fakeRepository.AddAsync(A<Pais>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
        Assert.True(result.Ativo);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnPaisResponse()
    {
        // Arrange
        var paisId = "BR";
        var pais = new Pais
        {
            Id = paisId,
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR",
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(paisId, A<CancellationToken>._))
            .Returns(Task.FromResult<Pais?>(pais));

        // Act
        var result = await _sut.GetByIdAsync(paisId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var paisId = "XX";
        A.CallTo(() => _fakeRepository.GetByIdAsync(paisId, A<CancellationToken>._))
            .Returns(Task.FromResult<Pais?>(null));

        // Act
        var result = await _sut.GetByIdAsync(paisId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ListAsync_ShouldReturnAllPaises()
    {
        // Arrange
        var paises = new List<Pais>
        {
            new() { Id = "BR", Nome = "Brasil", CodigoISO3 = "BRA", CodigoONU = 76, CodigoDDI = "+55", CodigoMoeda = "BRL", DefaultLocale = "pt-BR", Ativo = true },
            new() { Id = "US", Nome = "Estados Unidos", CodigoISO3 = "USA", CodigoONU = 840, CodigoDDI = "+1", CodigoMoeda = "USD", DefaultLocale = "en-US", Ativo = true }
        };

        A.CallTo(() => _fakeRepository.GetAllAsync(A<CancellationToken>._))
            .Returns(Task.FromResult<IEnumerable<Pais>>(paises));

        // Act
        var result = await _sut.ListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_WithValidRequest_ShouldUpdatePais()
    {
        // Arrange
        var paisId = "BR";
        var updateRequest = new UpdatePaisRequest { Nome = "Brasil Atualizado" };
        var pais = new Pais
        {
            Id = paisId,
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR",
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(paisId, A<CancellationToken>._))
            .Returns(Task.FromResult<Pais?>(pais));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Pais>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.UpdateAsync(paisId, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Brasil Atualizado", result.Nome);
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_ShouldSoftDeletePais()
    {
        // Arrange
        var paisId = "BR";
        var pais = new Pais
        {
            Id = paisId,
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR",
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(paisId, A<CancellationToken>._))
            .Returns(Task.FromResult<Pais?>(pais));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Pais>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.RemoveAsync(paisId);

        // Assert
        Assert.True(result);
        Assert.False(pais.Ativo);
    }
}

