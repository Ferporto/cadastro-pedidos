using CadastroPedidos.Domain.Entities;
using CadastroPedidos.Domain.Utils.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CadastroPedidos.Infrastructure.Context;

public class CadastroPedidosDbContext : DbContext, IDbContextWithTransactions
{
    public DbSet<Pedido> Pedido { get; set; }
    public DbSet<ItensPedido> ItensPedido { get; set; }
    public DbSet<Produto> Produto { get; set; }

    public CadastroPedidosDbContext(DbContextOptions<CadastroPedidosDbContext> options) : base(options)
    {
    }

    public IDbContextTransaction BeginTransaction()
    {
        return Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Database.CurrentTransaction?.Commit();
    }

    public void RollbackTransaction()
    {
        Database.CurrentTransaction?.Rollback();
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