using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Contexto de banco de dados para a aplicação de Geografia.
/// </summary>
public class GeografiaDbContext : DbContext
{
    /// <summary>
    /// Inicializa uma nova instância do <see cref="GeografiaDbContext"/>.
    /// </summary>
    public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : base(options)
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
    /// Configura as entidades e relacionamentos.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(2).IsFixedLength();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CodigoISO3).HasMaxLength(3).IsFixedLength().IsRequired();
            entity.Property(e => e.CodigoONU).IsRequired();
            entity.Property(e => e.CodigoDDI).HasMaxLength(10).IsRequired();
            entity.Property(e => e.CodigoMoeda).HasMaxLength(3).IsFixedLength().IsRequired();
            entity.Property(e => e.DefaultLocale).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.HasIndex(e => e.CodigoISO3).IsUnique();
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(6).IsFixedLength();
            entity.Property(e => e.PaisId).HasMaxLength(2).IsFixedLength().IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Sigla).HasMaxLength(3).IsRequired();
            entity.Property(e => e.Tipo).HasConversion<string>().HasMaxLength(20).IsRequired();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.HasOne(e => e.Pais)
                  .WithMany(p => p.Estados)
                  .HasForeignKey(e => e.PaisId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(e => e.PaisId);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EstadoId).HasMaxLength(6).IsFixedLength().IsRequired();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CodigoPostal).HasMaxLength(20);
            entity.Property(e => e.Latitude).HasPrecision(10, 8).IsRequired();
            entity.Property(e => e.Longitude).HasPrecision(11, 8).IsRequired();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.HasOne(e => e.Estado)
                  .WithMany(s => s.Cidades)
                  .HasForeignKey(e => e.EstadoId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(e => e.EstadoId);
            entity.HasIndex(e => e.Nome);
        });
    }
}
