using Confluent.Kafka;
using GeografiaService.Application.Services;
using GeografiaService.Infrastructure.Data;
using GeografiaService.Infrastructure.Events;
using GeografiaService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços à aplicação
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar Entity Framework e PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Host=localhost;Port=5432;Database=Geografia;Username=postgres;Password=postgres";
builder.Services.AddDbContext<GeografiaDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configurar Kafka
var kafkaConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };
builder.Services.AddSingleton<IProducer<string, string>>(sp => new ProducerBuilder<string, string>(kafkaConfig).Build());
builder.Services.AddScoped<IEventProducer, KafkaEventProducer>();

// Registrar Repositories
builder.Services.AddScoped<IPaisRepository, PaisRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<ICidadeRepository, CidadeRepository>();

// Registrar Services
builder.Services.AddScoped<PaisService>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<CidadeService>();

// Configurar autenticação JWT (comentado)
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority = builder.Configuration["Jwt:Authority"];
//         options.Audience = builder.Configuration["Jwt:Audience"];
//     });
// builder.Services.AddAuthorization();

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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware de autenticação JWT (comentado)
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();

