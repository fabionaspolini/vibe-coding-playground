using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GeografiaService.Infrastructure.Data;

/// <summary>
/// Factory para criar instâncias do DbContext no tempo de design.
/// Utilizado pelo Entity Framework Core para gerar migrations.
/// </summary>
public class GeografiaDbContextFactory : IDesignTimeDbContextFactory<GeografiaDbContext>
{
    /// <summary>
    /// Cria uma instância do DbContext para operações de design.
    /// </summary>
    /// <param name="args">Argumentos passados via linha de comando.</param>
    /// <returns>Uma instância configurada do DbContext.</returns>
    public GeografiaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GeografiaDbContext>();
        
        var connectionString = "Host=localhost;Port=5432;Database=Geografia;Username=postgres;Password=postgres";
        optionsBuilder.UseNpgsql(connectionString);

        return new GeografiaDbContext(optionsBuilder.Options);
    }
}

