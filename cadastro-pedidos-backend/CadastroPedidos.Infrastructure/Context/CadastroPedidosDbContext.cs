using CadastroPedidos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Infrastructure.Context;

public class CadastroPedidosDbContext : DbContext
{
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<ItensPedido> ItensPedido { get; set; }
    public DbSet<Produto> Produto { get; set; }

    public CadastroPedidosDbContext(DbContextOptions<CadastroPedidosDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>().ToTable("Pedido");
        modelBuilder.Entity<ItensPedido>().ToTable("ItensPedido");
        modelBuilder.Entity<Produto>().ToTable("Produto");

        modelBuilder.Entity<ItensPedido>()
            .HasOne(item => item.Pedido)
            .WithMany(pedido => pedido.ItensPedido)
            .HasForeignKey(item => item.IdPedido)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItensPedido>()
            .HasOne(item => item.Produto)
            .WithMany(pedido => pedido.ItensPedido)
            .HasForeignKey(item => item.IdProduto)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}