using Geografia.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geografia.Api.Data;

/// <summary>
/// Contexto de banco de dados da aplicação Geografia.
/// </summary>
public class GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Tabela de Países.
    /// </summary>
    public DbSet<Pais> Paises => Set<Pais>();

    /// <summary>
    /// Tabela de Estados.
    /// </summary>
    public DbSet<Estado> Estados => Set<Estado>();

    /// <summary>
    /// Tabela de Cidades.
    /// </summary>
    public DbSet<Cidade> Cidades => Set<Cidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações adicionais se necessário
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasIndex(e => e.Nome).IsUnique();
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.Property(e => e.Tipo).HasConversion<string>();
        });
    }
}
