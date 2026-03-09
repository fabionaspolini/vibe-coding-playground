namespace Geografia.Infrastructure.Data;

using Geografia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Contexto do Entity Framework Core para a aplicação de Geografia.
/// </summary>
public class GeografiaDbContext : DbContext
{
    /// <summary>
    /// Construtor do contexto de banco de dados.
    /// </summary>
    /// <param name="options">Opções de configuração do contexto.</param>
    public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// DbSet de países.
    /// </summary>
    public required DbSet<Pais> Paises { get; set; }

    /// <summary>
    /// DbSet de estados.
    /// </summary>
    public required DbSet<Estado> Estados { get; set; }

    /// <summary>
    /// DbSet de cidades.
    /// </summary>
    public required DbSet<Cidade> Cidades { get; set; }

    /// <summary>
    /// Configuração do modelo de dados.
    /// </summary>
    /// <param name="modelBuilder">Construtor do modelo.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de Pais
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasMaxLength(2).IsRequired();
            entity.Property(p => p.Nome).IsRequired();
            entity.Property(p => p.CodigoISO3).HasMaxLength(3).IsRequired();
            entity.Property(p => p.CodigoONU).IsRequired();
            entity.Property(p => p.CodigoDDI).IsRequired();
            entity.Property(p => p.CodigoMoeda).HasMaxLength(3).IsRequired();
            entity.Property(p => p.DefaultLocale).IsRequired();
            entity.Property(p => p.Ativo).IsRequired();
        });

        // Configuração de Estado
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(6).IsRequired();
            entity.Property(e => e.PaisId).HasMaxLength(2).IsRequired();
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Sigla).IsRequired();
            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.Ativo).IsRequired();
            
            entity.HasOne<Pais>()
                .WithMany()
                .HasForeignKey(e => e.PaisId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração de Cidade
        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired();
            entity.Property(c => c.EstadoId).HasMaxLength(6).IsRequired();
            entity.Property(c => c.Nome).IsRequired();
            entity.Property(c => c.CodigoPostal).IsRequired();
            entity.Property(c => c.Latitude).HasPrecision(10, 7).IsRequired();
            entity.Property(c => c.Longitude).HasPrecision(10, 7).IsRequired();
            entity.Property(c => c.Ativo).IsRequired();
            
            entity.HasOne<Estado>()
                .WithMany()
                .HasForeignKey(c => c.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

