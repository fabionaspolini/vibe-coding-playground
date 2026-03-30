using FakeItEasy;
using Geografia.Api.Controllers;
using Geografia.Api.Data;
using Geografia.Api.Domain.Entities;
using Geografia.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Geografia.Tests;

public class PaisesControllerTests
{
    [Fact]
    public async Task GetById_ReturnsCountry_WhenExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GeografiaDbContext>()
            .UseInMemoryDatabase(databaseName: "GeografiaDb_Test")
            .Options;

        using var context = new GeografiaDbContext(options);
        var country = new Pais 
        { 
            Id = "BR", 
            Nome = "Brasil", 
            CodigoISO3 = "BRA", 
            CodigoDDI = "+55", 
            CodigoMoeda = "BRL", 
            DefaultLocale = "pt-BR" 
        };
        context.Paises.Add(country);
        await context.SaveChangesAsync();

        var kafkaMock = A.Fake<KafkaProducerService>();
        var controller = new PaisesController(context, kafkaMock);

        // Act
        var result = await controller.GetById("BR");

        // Assert
        Assert.NotNull(result.Value);
        Assert.Equal("Brasil", result.Value.Nome);
    }
}
