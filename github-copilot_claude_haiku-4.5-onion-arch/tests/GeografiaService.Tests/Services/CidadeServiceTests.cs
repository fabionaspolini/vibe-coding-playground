using FakeItEasy;
using GeografiaService.Application.DTOs;
using GeografiaService.Application.Services;
using GeografiaService.Domain.Entities;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Xunit;

namespace GeografiaService.Tests.Services;

/// <summary>
/// Testes unitários para o serviço de Cidade.
/// </summary>
public class CidadeServiceTests
{
    private readonly ICidadeRepository _fakeRepository;
    private readonly IEventProducer _fakeEventProducer;
    private readonly ILogger<CidadeService> _fakeLogger;
    private readonly CidadeService _sut;

    public CidadeServiceTests()
    {
        _fakeRepository = A.Fake<ICidadeRepository>();
        _fakeEventProducer = A.Fake<IEventProducer>();
        _fakeLogger = A.Fake<ILogger<CidadeService>>();
        _sut = new CidadeService(_fakeRepository, _fakeEventProducer, _fakeLogger);
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldReturnCidadeResponse()
    {
        // Arrange
        var request = new CreateCidadeRequest
        {
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01310-100",
            Latitude = -23.5505m,
            Longitude = -46.6333m
        };

        A.CallTo(() => _fakeRepository.AddAsync(A<Cidade>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("São Paulo", result.Nome);
        Assert.Equal("01310-100", result.CodigoPostal);
        Assert.True(result.Ativo);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnCidadeResponse()
    {
        // Arrange
        var cidadeId = Guid.NewGuid();
        var cidade = new Cidade
        {
            Id = cidadeId,
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01310-100",
            Latitude = -23.5505m,
            Longitude = -46.6333m,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(cidadeId, A<CancellationToken>._))
            .Returns(Task.FromResult<Cidade?>(cidade));

        // Act
        var result = await _sut.GetByIdAsync(cidadeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("São Paulo", result.Nome);
        Assert.Equal("01310-100", result.CodigoPostal);
    }

    [Fact]
    public async Task ListAsync_WithEstadoIdFilter_ShouldReturnCidadesFromEstado()
    {
        // Arrange
        var estadoId = "BR-SP";
        var cidades = new List<Cidade>
        {
            new() { Id = Guid.NewGuid(), EstadoId = estadoId, Nome = "São Paulo", CodigoPostal = "01310-100", Latitude = -23.5505m, Longitude = -46.6333m, Ativo = true },
            new() { Id = Guid.NewGuid(), EstadoId = estadoId, Nome = "Campinas", CodigoPostal = "13010-000", Latitude = -22.9099m, Longitude = -47.0626m, Ativo = true }
        };

        A.CallTo(() => _fakeRepository.GetAllAsync(A<CancellationToken>._))
            .Returns(Task.FromResult<IEnumerable<Cidade>>(cidades));

        // Act
        var result = await _sut.ListAsync(estadoId: estadoId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, c => Assert.Equal(estadoId, c.EstadoId));
    }

    [Fact]
    public async Task UpdateAsync_WithValidRequest_ShouldUpdateCidade()
    {
        // Arrange
        var cidadeId = Guid.NewGuid();
        var updateRequest = new UpdateCidadeRequest { Nome = "São Paulo Atualizada" };
        var cidade = new Cidade
        {
            Id = cidadeId,
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01310-100",
            Latitude = -23.5505m,
            Longitude = -46.6333m,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(cidadeId, A<CancellationToken>._))
            .Returns(Task.FromResult<Cidade?>(cidade));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Cidade>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.UpdateAsync(cidadeId, updateRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("São Paulo Atualizada", result.Nome);
    }

    [Fact]
    public async Task RemoveAsync_WithValidId_ShouldSoftDeleteCidade()
    {
        // Arrange
        var cidadeId = Guid.NewGuid();
        var cidade = new Cidade
        {
            Id = cidadeId,
            EstadoId = "BR-SP",
            Nome = "São Paulo",
            CodigoPostal = "01310-100",
            Latitude = -23.5505m,
            Longitude = -46.6333m,
            Ativo = true
        };

        A.CallTo(() => _fakeRepository.GetByIdAsync(cidadeId, A<CancellationToken>._))
            .Returns(Task.FromResult<Cidade?>(cidade));
        A.CallTo(() => _fakeRepository.UpdateAsync(A<Cidade>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);
        A.CallTo(() => _fakeEventProducer.ProduceEventAsync(A<string>._, A<string>._, A<string>._, A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.RemoveAsync(cidadeId);

        // Assert
        Assert.True(result);
        Assert.False(cidade.Ativo);
    }
}

