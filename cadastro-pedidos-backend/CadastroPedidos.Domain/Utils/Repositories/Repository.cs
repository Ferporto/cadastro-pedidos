using CadastroPedidos.Domain.Utils.Dependencies;
using Microsoft.EntityFrameworkCore;

namespace CadastroPedidos.Domain.Utils.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IScopedDependency
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
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

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
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
