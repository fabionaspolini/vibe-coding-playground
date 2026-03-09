using GeografiaService.Domain.Entities;
using GeografiaService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GeografiaService.Infrastructure.Data;

/// <summary>
/// DbContext da aplicação de Geografia.
/// Gerencia a configuração e acesso aos dados de Países, Estados e Cidades.
/// </summary>
public class GeografiaDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do contexto de dados.
    /// </summary>
    /// <param name="options">Opções de configuração do DbContext.</param>
    public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : base(options) { }

    /// <summary>
    /// DbSet que representa a tabela de Países.
    /// </summary>
    public DbSet<Pais> Paises { get; set; } = default!;

    /// <summary>
    /// DbSet que representa a tabela de Estados.
    /// </summary>
    public DbSet<Estado> Estados { get; set; } = default!;

    /// <summary>
    /// DbSet que representa a tabela de Cidades.
    /// </summary>
    public DbSet<Cidade> Cidades { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarPais(modelBuilder);
        ConfigurarEstado(modelBuilder);
        ConfigurarCidade(modelBuilder);
    }

    private static void ConfigurarPais(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pais>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Pais>()
            .Property(p => p.Id)
            .HasMaxLength(2)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.Nome)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.CodigoISO3)
            .HasMaxLength(3)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.CodigoONU)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.CodigoDDI)
            .HasMaxLength(10)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.CodigoMoeda)
            .HasMaxLength(3)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.DefaultLocale)
            .HasMaxLength(10)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .Property(p => p.Ativo)
            .HasDefaultValue(true)
            .IsRequired();

        modelBuilder.Entity<Pais>()
            .HasMany(p => p.Estados)
            .WithOne(e => e.Pais)
            .HasForeignKey(e => e.PaisId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pais>()
            .HasIndex(p => p.Nome)
            .IsUnique();
    }

    private static void ConfigurarEstado(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Estado>()
            .Property(e => e.Id)
            .HasMaxLength(6)
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .Property(e => e.PaisId)
            .HasMaxLength(2)
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .Property(e => e.Nome)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .Property(e => e.Sigla)
            .HasMaxLength(10)
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .Property(e => e.Tipo)
            .HasConversion<string>()
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .Property(e => e.Ativo)
            .HasDefaultValue(true)
            .IsRequired();

        modelBuilder.Entity<Estado>()
            .HasMany(e => e.Cidades)
            .WithOne(c => c.Estado)
            .HasForeignKey(c => c.EstadoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Estado>()
            .HasIndex(e => e.Sigla);

        modelBuilder.Entity<Estado>()
            .HasIndex(e => e.Nome);
    }

    private static void ConfigurarCidade(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cidade>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Cidade>()
            .Property(c => c.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.EstadoId)
            .HasMaxLength(6)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.Nome)
            .HasMaxLength(256)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.CodigoPostal)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.Latitude)
            .HasPrecision(9, 6)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.Longitude)
            .HasPrecision(9, 6)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .Property(c => c.Ativo)
            .HasDefaultValue(true)
            .IsRequired();

        modelBuilder.Entity<Cidade>()
            .HasIndex(c => c.Nome);

        modelBuilder.Entity<Cidade>()
            .HasIndex(c => c.CodigoPostal)
            .IsUnique();
    }
}

