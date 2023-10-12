using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.Repositories.Implement;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }


    public async Task<TEntity?> GetByIdAsync(int id, string includeProperties = "", bool disableTracking = true)
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        var result = await query.FirstOrDefaultAsync(t => t.Id == id);

        return result;
    }

    public async Task<List<TEntity>> GetAllAsync(string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var includeProperty in includeProperties.Split
                     (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }


    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        EntityEntry<TEntity> resultEntity = await _dbSet.AddAsync(entity);
        return resultEntity.Entity;
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<bool> ExistByIdAsync(int id)
    {
        var user = await GetByIdAsync(id);
        return user != null;
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }
}