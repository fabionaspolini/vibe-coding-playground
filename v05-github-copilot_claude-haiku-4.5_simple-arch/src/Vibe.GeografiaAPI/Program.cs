using Microsoft.EntityFrameworkCore;
using Vibe.GeografiaAPI.Application.Data;
using Vibe.GeografiaAPI.Application.Events;

var builder = WebApplication.CreateBuilder(args);

// Registrar serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "API de Gerenciamento de Dados Geográficos",
        Version = "v1",
        Description = "API REST para gerenciamento de países, estados e cidades com integração Kafka"
    });
});

// Configurar DbContext com PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Port=5432;Database=v05-github-copilot_claude-haiku-4.5_simple-arch;Username=postgres;Password=123456";

builder.Services.AddDbContext<GeografiaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar Kafka Event Producer como Singleton
builder.Services.AddSingleton<KafkaEventProducer>();

var app = builder.Build();

// Aplicar migrations em desenvolvimento
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<GeografiaDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configurar o pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geografia API v1"));

// Descomentar as linhas abaixo para habilitar autenticação JWT
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();

