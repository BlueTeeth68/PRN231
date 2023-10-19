using DataAccess.Models;

namespace DataAccess.Repositories.Interface;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(object id);

    Task<List<TEntity>> GetAllAsync(string includeProperties = "");

    Task<TEntity?> AddAsync(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(object id);

    Task<bool> ExistByIdAsync(object id);

    Task<int> SaveChangeAsync();
}