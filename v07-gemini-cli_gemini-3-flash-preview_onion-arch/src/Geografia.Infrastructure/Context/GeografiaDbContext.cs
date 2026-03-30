using Geografia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geografia.Infrastructure.Context;

public class GeografiaDbContext(DbContextOptions<GeografiaDbContext> options) : DbContext(options)
{
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<Cidade> Cidades => Set<Cidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(2);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.CodigoISO3).HasMaxLength(3);
            entity.Property(e => e.CodigoMoeda).HasMaxLength(3);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(6);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Sigla).IsRequired();
            entity.Property(e => e.Tipo).HasConversion<string>();

            entity.HasOne(e => e.Pais)
                .WithMany()
                .HasForeignKey(e => e.PaisId);
        });

        modelBuilder.Entity<Cidade>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired();

            entity.HasOne(e => e.Estado)
                .WithMany()
                .HasForeignKey(e => e.EstadoId);
        });
    }
}
