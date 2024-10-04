using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Domain.Utils.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly IDbContextWithTransactions _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(IDbContextWithTransactions context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var createdEntity = await _dbSet.AddAsync(entity);
        return createdEntity.Entity;
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }
}
