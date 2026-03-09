using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FakeItEasy;
using Xunit;

namespace Geografia.Tests.Services;

/// <summary>
/// Testes unitários para <see cref="PaisService"/>.
/// </summary>
public class PaisServiceTests
{
    private readonly IPaisRepository _repository;
    private readonly IKafkaEventService _kafkaService;
    private readonly PaisService _service;

    public PaisServiceTests()
    {
        _repository = A.Fake<IPaisRepository>();
        _kafkaService = A.Fake<IKafkaEventService>();
        _service = new PaisService(_repository, _kafkaService);
    }

    /// <summary>
    /// Testa se o método CreateAsync chama o repositório e o Kafka.
    /// </summary>
    [Fact]
    public async Task CreateAsync_DeveChamarRepositorioEKafka()
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

        var paisCriado = new Pais
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

        A.CallTo(() => _repository.CreateAsync(A<Pais>._, A<CancellationToken>._))
            .Returns(Task.FromResult(paisCriado));

        // Act
        var resultado = await _service.CreateAsync(request);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("BR", resultado.Id);
        Assert.Equal("Brasil", resultado.Nome);
        A.CallTo(() => _repository.CreateAsync(A<Pais>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _kafkaService.PublishCreate("geografia.pais", "BR", A<Pais>._)).MustHaveHappenedOnceExactly();
    }

    /// <summary>
    /// Testa se o método GetByIdAsync retorna o país correto.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_QuandoPaisExiste_DeveRetornarPais()
    {
        // Arrange
        var pais = new Pais { Id = "BR", Nome = "Brasil", CodigoISO3 = "BRA", CodigoONU = 76, CodigoDDI = "+55", CodigoMoeda = "BRL", DefaultLocale = "pt-BR" };
        A.CallTo(() => _repository.GetByIdAsync("BR", A<CancellationToken>._)).Returns(pais);

        // Act
        var resultado = await _service.GetByIdAsync("BR");

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("BR", resultado.Id);
    }

    /// <summary>
    /// Testa se o método GetByIdAsync retorna null quando país não existe.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_QuandoPaisNaoExiste_DeveRetornarNull()
    {
        // Arrange
        A.CallTo(() => _repository.GetByIdAsync("XX", A<CancellationToken>._)).Returns((Pais?)null);

        // Act
        var resultado = await _service.GetByIdAsync("XX");

        // Assert
        Assert.Null(resultado);
    }

    /// <summary>
    /// Testa se o método ListAsync retorna todos os países.
    /// </summary>
    [Fact]
    public async Task ListAsync_DeveRetornarTodosPaises()
    {
        // Arrange
        var paises = new List<Pais>
        {
            new() { Id = "BR", Nome = "Brasil", CodigoISO3 = "BRA", CodigoONU = 76, CodigoDDI = "+55", CodigoMoeda = "BRL", DefaultLocale = "pt-BR" },
            new() { Id = "US", Nome = "Estados Unidos", CodigoISO3 = "USA", CodigoONU = 840, CodigoDDI = "+1", CodigoMoeda = "USD", DefaultLocale = "en-US" }
        };
        A.CallTo(() => _repository.ListAsync(A<CancellationToken>._)).Returns(paises);

        // Act
        var resultado = await _service.ListAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
    }

    /// <summary>
    /// Testa se o método UpdateAsync atualiza o país e publica evento Kafka.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_DeveAtualizarEPublicarKafka()
    {
        // Arrange
        var paisExistente = new Pais
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

        A.CallTo(() => _repository.GetByIdAsync("BR", A<CancellationToken>._)).Returns(paisExistente);
        A.CallTo(() => _repository.UpdateAsync(A<Pais>._, A<CancellationToken>._))
            .ReturnsLazily(call => Task.FromResult(call.GetArgument<Pais>(0)!));

        // Act
        var resultado = await _service.UpdateAsync("BR", request);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Brasil Atualizado", resultado.Nome);
        A.CallTo(() => _kafkaService.PublishUpdate("geografia.pais", "BR", A<Pais>._)).MustHaveHappenedOnceExactly();
    }

    /// <summary>
    /// Testa se o método UpdateAsync lança exceção quando país não existe.
    /// </summary>
    [Fact]
    public async Task UpdateAsync_QuandoPaisNaoExiste_DeveLancarExcecao()
    {
        // Arrange
        A.CallTo(() => _repository.GetByIdAsync("XX", A<CancellationToken>._)).Returns((Pais?)null);
        var request = new UpdatePaisRequest { Nome = "Teste", CodigoISO3 = "TST", CodigoONU = 1, CodigoDDI = "+0", CodigoMoeda = "XXX", DefaultLocale = "xx" };

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync("XX", request));
    }

    /// <summary>
    /// Testa se o método RemoveAsync remove o país e publica evento Kafka.
    /// </summary>
    [Fact]
    public async Task RemoveAsync_DeveRemoverEPublicarKafka()
    {
        // Arrange
        A.CallTo(() => _repository.RemoveAsync("BR", A<CancellationToken>._)).Returns(true);

        // Act
        var resultado = await _service.RemoveAsync("BR");

        // Assert
        Assert.True(resultado);
        A.CallTo(() => _kafkaService.PublishDelete("geografia.pais", "BR")).MustHaveHappenedOnceExactly();
    }

    /// <summary>
    /// Testa se o método RemoveAsync retorna false quando país não existe.
    /// </summary>
    [Fact]
    public async Task RemoveAsync_QuandoPaisNaoExiste_DeveRetornarFalse()
    {
        // Arrange
        A.CallTo(() => _repository.RemoveAsync("XX", A<CancellationToken>._)).Returns(false);

        // Act
        var resultado = await _service.RemoveAsync("XX");

        // Assert
        Assert.False(resultado);
    }
}
