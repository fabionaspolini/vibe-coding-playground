using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Kafka;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// Classe de extensão para registro de dependências da camada de Infrastructure.
/// </summary>
public static class InfrastructureDependencyInjection
{
    /// <summary>
    /// Adiciona os serviços de infraestrutura ao container de injeção de dependência.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

        services.AddDbContext<GeografiaDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IPaisRepository, PaisRepository>();
        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<ICidadeRepository, CidadeRepository>();

        var kafkaBootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers") ?? "localhost:9092";
        services.AddSingleton<IKafkaEventService>(sp =>
        {
            var logger = sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<KafkaEventService>>();
            return new KafkaEventService(logger, kafkaBootstrapServers);
        });

        return services;
    }
}
