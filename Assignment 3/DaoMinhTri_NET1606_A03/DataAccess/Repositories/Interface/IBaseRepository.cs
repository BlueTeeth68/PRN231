using DataAccess.Models;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Interface;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(object id);

    Task<List<TEntity>> GetAllAsync(string includeProperties = "");

    IQueryable<TEntity> GetAllOdataAsync();

    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        string includeProperties,
        bool disableTracking = false
        );

    Task<TEntity?> AddAsync(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(object id);

    Task<bool> ExistByIdAsync(object id);

    Task<int> SaveChangeAsync();
}