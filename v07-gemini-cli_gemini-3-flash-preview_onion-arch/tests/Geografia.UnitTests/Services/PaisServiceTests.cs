using FakeItEasy;
using Geografia.Application.Dtos;
using Geografia.Application.Interfaces;
using Geografia.Application.Services;
using Geografia.Domain.Entities;
using Geografia.Domain.Repositories;
using Xunit;

namespace Geografia.UnitTests.Services;

public class PaisServiceTests
{
    private readonly IPaisRepository _repository;
    private readonly IKafkaProducer _producer;
    private readonly PaisService _service;

    public PaisServiceTests()
    {
        _repository = A.Fake<IPaisRepository>();
        _producer = A.Fake<IKafkaProducer>();
        _service = new PaisService(_repository, _producer);
    }

    [Fact]
    public async Task Create_Should_Add_Pais_And_Produce_Message()
    {
        // Arrange
        var request = new PaisRequest
        {
            Id = "BR",
            Nome = "Brasil",
            CodigoISO3 = "BRA",
            CodigoONU = 76,
            CodigoDDI = "+55",
            CodigoMoeda = "BRL",
            DefaultLocale = "pt-BR"
        };

        // Act
        var result = await _service.Create(request);

        // Assert
        A.CallTo(() => _repository.AddAsync(A<Pais>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _repository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _producer.Produce(A<string>._, A<string>._, A<PaisDto>._)).MustHaveHappenedOnceExactly();
        
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(request.Nome, result.Nome);
    }
}
