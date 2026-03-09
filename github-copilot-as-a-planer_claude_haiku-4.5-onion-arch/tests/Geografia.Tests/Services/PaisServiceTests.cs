namespace Geografia.Tests.Services;

using Geografia.Application.DTOs;
using Geografia.Application.Services;
using Geografia.Domain.Entities;
using Geografia.Infrastructure.Kafka;
using Geografia.Infrastructure.Repositories;
using FakeItEasy;
using Xunit;

/// <summary>
/// Testes unitários para o serviço de Pais.
/// </summary>
public class PaisServiceTests
{
    private readonly IRepository<Pais> _repository;
    private readonly IKafkaProducer _kafkaProducer;
    private readonly PaisService _service;

    public PaisServiceTests()
    {
        _repository = A.Fake<IRepository<Pais>>();
        _kafkaProducer = A.Fake<IKafkaProducer>();
        _service = new PaisService(_repository, _kafkaProducer);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddPaisAndProduceEvent()
    {
        // Arrange
        var dto = new CriarPaisDto
        {
            Id = "BR",
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR"
        };

        A.CallTo(() => _repository.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
        Assert.True(result.Ativo);
        
        A.CallTo(() => _repository.AddAsync(A<Pais>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _kafkaProducer.Produce(A<string>.Ignored, A<string>.Ignored, A<dynamic>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetByIdAsync_WhenPaisExists_ShouldReturnPais()
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

        A.CallTo(() => _repository.GetByIdAsync("BR")).Returns(Task.FromResult<Pais?>(pais));

        // Act
        var result = await _service.GetByIdAsync("BR");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BR", result.Id);
        Assert.Equal("Brasil", result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_WhenPaisDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        A.CallTo(() => _repository.GetByIdAsync("XX")).Returns(Task.FromResult<Pais?>(null));

        // Act
        var result = await _service.GetByIdAsync("XX");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ListAsync_ShouldReturnFilteredPaises()
    {
        // Arrange
        var paises = new List<Pais>
        {
            new Pais
            {
                Id = "BR",
                Nome = "Brasil",
                CodigoISO3 = "BRA",
                CodigoONU = 76,
                CodigoDDI = "+55",
                CodigoMoeda = "BRL",
                DefaultLocale = "pt-BR",
                Ativo = true
            }
        };

        A.CallTo(() => _repository.ListAsync(A<Dictionary<string, object>>.Ignored))
            .Returns(Task.FromResult(paises));

        // Act
        var result = await _service.ListAsync();

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task RemoveAsync_ShouldDeactivatePais()
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

        A.CallTo(() => _repository.GetByIdAsync("BR")).Returns(Task.FromResult<Pais?>(pais));
        A.CallTo(() => _repository.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _service.RemoveAsync("BR");

        // Assert
        Assert.False(pais.Ativo);
        A.CallTo(() => _repository.UpdateAsync(A<Pais>.Ignored)).MustHaveHappenedOnceExactly();
    }
}

