using CadastroPedidos.Domain.Utils.Repositories;

namespace CadastroPedidos.Domain.Utils.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    // Iniciar uma transação
    IUnitOfWork Begin();

    // Completar a transação (Commit)
    Task CompleteAsync();

    // Cancelar a transação (Rollback)
    void Rollback();
}
