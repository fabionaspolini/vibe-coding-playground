using Microsoft.EntityFrameworkCore;
using Vibe.GeografiaAPI.Domain.Entities;

namespace Vibe.GeografiaAPI.Application.Data;

/// <summary>
/// Contexto do banco de dados para a aplicação de gerenciamento de dados geográficos.
/// Gerencia as entidades de País, Estado e Cidade.
/// </summary>
public class GeografiaDbContext : DbContext
{
    /// <summary>
    /// Initializa uma nova instância do contexto com as opções especificadas.
    /// </summary>
    public GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : base(options) { }

    /// <summary>
    /// DbSet para a entidade País.
    /// </summary>
    public DbSet<Pais> Paises => Set<Pais>();

    /// <summary>
    /// DbSet para a entidade Estado.
    /// </summary>
    public DbSet<Estado> Estados => Set<Estado>();

    /// <summary>
    /// DbSet para a entidade Cidade.
    /// </summary>
    public DbSet<Cidade> Cidades => Set<Cidade>();

    /// <summary>
    /// Configura o modelo de banco de dados definindo chaves primárias e relacionamentos.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Pais
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(2);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CodigoISO3).HasMaxLength(3).IsRequired();
            entity.Property(e => e.CodigoONU).IsRequired();
            entity.Property(e => e.CodigoDDI).IsRequired();
            entity.Property(e => e.CodigoMoeda).HasMaxLength(3).IsRequired();
            entity.Property(e => e.DefaultLocale).IsRequired();
            entity.Property(e => e.Ativo).HasDefaultValue(true);
        });

        // Configuração da entidade Estado
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(6);
            entity.Property(e => e.PaisId).HasMaxLength(2).IsRequired();
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Sigla).IsRequired();
            entity.Property(e => e.Tipo).IsRequired();
            entity.Property(e => e.Ativo).HasDefaultValue(true);

            // Relacionamento com Pais
            entity.HasOne(e => e.Pais)
                .WithMany()
                .HasForeignKey(e => e.PaisId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração da entidade Cidade
        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.EstadoId).HasMaxLength(6).IsRequired();
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CodigoPostal).IsRequired();
            entity.Property(e => e.Latitude).HasPrecision(9, 6);
            entity.Property(e => e.Longitude).HasPrecision(9, 6);
            entity.Property(e => e.Ativo).HasDefaultValue(true);

            // Relacionamento com Estado
            entity.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

