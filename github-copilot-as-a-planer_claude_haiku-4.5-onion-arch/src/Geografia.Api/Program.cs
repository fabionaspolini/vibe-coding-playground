using Geografia.Application.Services;
using Geografia.Infrastructure.Data;
using Geografia.Infrastructure.Kafka;
using Geografia.Infrastructure.Repositories;
using Geografia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Seção 'DefaultConnection' não encontrada no appsettings.");

builder.Services.AddDbContext<GeografiaDbContext>(options =>
    options.UseNpgsql(connectionString, npgOptions =>
        npgOptions.MigrationsAssembly("Geografia.Infrastructure")));

// Configuração de Kafka
var kafkaBootstrapServers = builder.Configuration.GetValue<string>("Kafka:BootstrapServers") ?? "localhost:9092";
builder.Services.AddSingleton<IKafkaProducer>(new KafkaProducer(kafkaBootstrapServers));

// Registrar repositórios genéricos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Registrar serviços
builder.Services.AddScoped<IPaisService, PaisService>();
builder.Services.AddScoped<IEstadoService, EstadoService>();
builder.Services.AddScoped<ICidadeService, CidadeService>();

// Adicionar controladores
builder.Services.AddControllers();

// Adicionar Swagger/OpenAPI
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "API de Geografia",
//         Version = "v1",
//         Description = "API para gerenciamento de dados geográficos (países, estados e cidades)"
//     });
// });

var app = builder.Build();

// Executar migrations em ambiente Development
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<GeografiaDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Geografia v1");
    });
}

app.UseHttpsRedirection();

// app.UseAuthorization(); // TODO: Ativar quando implementar autenticação JWT

app.MapControllers();

app.Run();

