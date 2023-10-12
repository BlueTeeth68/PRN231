using DataAccess.Models;

namespace DataAccess.Repositories.Interface;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(int id, string includeProperties = "", bool disableTracking = true);

    Task<List<TEntity>> GetAllAsync(string includeProperties = "");

    Task<TEntity?> AddAsync(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(int id);

    Task<bool> ExistByIdAsync(int id);

    Task<int> SaveChangeAsync();
}