using CadastroPedidos.Domain.Utils.Dependencies;
using CadastroPedidos.Domain.Utils.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CadastroPedidos.Domain.Utils.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private IDbContextTransaction _transaction;
    private Dictionary<string, object> _repositories;

    public UnitOfWork(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<string, object>();
    }

    // Iniciar a transação e retornar a própria instância
    public IUnitOfWork Begin()
    {
        _transaction = _context.Database.BeginTransaction();
        return this;
    }

    // Retorna o repositório correspondente à entidade solicitada
    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IRepository<TEntity>)_repositories[type];
    }

    // Completar a transação e realizar commit
    public async Task CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            _transaction?.Commit();
        }
        catch
        {
            _transaction?.Rollback();
            throw;
        }
    }

    // Reverter transações caso necessário
    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
