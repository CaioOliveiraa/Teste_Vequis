using LeitorNotasFiscais.Models;
using Microsoft.EntityFrameworkCore;

namespace LeitorNotasFiscais.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    // Só aplica SQLite se ainda não estiver configurado (testes evitam isso)
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite("Data Source=notas.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Cliente>()
            .ToTable("TB_CLIENTE")
            .Property(c => c.Id)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        modelBuilder
            .Entity<Produto>()
            .ToTable("TB_PRODUTO")
            .Property(p => p.Id)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Cliente>().Property(c => c.Nome).HasColumnName("NOME");
        modelBuilder.Entity<Cliente>().Property(c => c.Cidade).HasColumnName("CIDADE");
        modelBuilder.Entity<Cliente>().Property(c => c.Estado).HasColumnName("ESTADO");
        modelBuilder.Entity<Cliente>().Property(c => c.Pais).HasColumnName("PAIS");

        modelBuilder.Entity<Produto>().Property(p => p.Nome).HasColumnName("NOME");
        modelBuilder.Entity<Produto>().Property(p => p.Valor).HasColumnName("VALOR");
    }
}
