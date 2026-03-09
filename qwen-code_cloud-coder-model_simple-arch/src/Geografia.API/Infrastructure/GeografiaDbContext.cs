using Geografia.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Geografia.API.Infrastructure;

/// <summary>
/// Contexto de dados da aplicação Geografia.
/// </summary>
public class GeografiaDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="GeografiaDbContext"/>.
    /// </summary>
    public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Conjunto de países.
    /// </summary>
    public DbSet<Pais> Paises { get; set; }

    /// <summary>
    /// Conjunto de estados.
    /// </summary>
    public DbSet<Estado> Estados { get; set; }

    /// <summary>
    /// Conjunto de cidades.
    /// </summary>
    public DbSet<Cidade> Cidades { get; set; }

    /// <summary>
    /// Configura as entidades e seus mapeamentos.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(2).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CodigoISO3).HasMaxLength(3).IsRequired();
            entity.Property(e => e.CodigoDDI).HasMaxLength(10).IsRequired();
            entity.Property(e => e.CodigoMoeda).HasMaxLength(3).IsRequired();
            entity.Property(e => e.DefaultLocale).HasMaxLength(20).IsRequired();
            entity.HasMany(e => e.Estados).WithOne(e => e.Pais).HasForeignKey(e => e.PaisId);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(6).IsRequired();
            entity.Property(e => e.PaisId).HasMaxLength(2).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Sigla).HasMaxLength(4).IsRequired();
            entity.HasMany(e => e.Cidades).WithOne(e => e.Estado).HasForeignKey(e => e.EstadoId);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EstadoId).HasMaxLength(6).IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CodigoPostal).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Latitude).HasPrecision(10, 8);
            entity.Property(e => e.Longitude).HasPrecision(11, 8);
        });
    }
}
