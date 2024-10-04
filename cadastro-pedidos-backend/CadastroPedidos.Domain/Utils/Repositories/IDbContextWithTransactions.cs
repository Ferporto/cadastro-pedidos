using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CadastroPedidos.Domain.Utils.Repositories;

public interface IDbContextWithTransactions : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbContextTransaction BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
